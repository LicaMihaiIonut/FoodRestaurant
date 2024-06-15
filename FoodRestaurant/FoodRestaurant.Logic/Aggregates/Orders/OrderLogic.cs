using FoodRestaurant.Abstractions.Aggregates.Orders;
using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Carts;
using FoodRestaurant.Domain.Aggregates.Orders;
using FoodRestaurant.Domain.Aggregates.Reviews;
using FoodRestaurant.Domain.Pages;

namespace FoodRestaurant.Logic.Aggregates.Orders;

public class OrderLogic : IOrderLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSessionProvider _userSessionProvider;
    private readonly IOrderDashboardFactory _orderDashboardFactory;

    public OrderLogic(IUnitOfWork unitOfWork, IOrderDashboardFactory orderDashboardFactory, IUserSessionProvider userSessionProvider)
    {
        _unitOfWork = unitOfWork;
        _orderDashboardFactory = orderDashboardFactory;
        _userSessionProvider = userSessionProvider;
    }

    public async Task AddReviewAsync(OrderReviewPostDto postDto)
    {
        var restaurant = await _unitOfWork.RestaurantRepository.GetByOrderId(postDto.OrderId);

        restaurant.AverageGrade = (restaurant.AverageGrade * restaurant.TotalReviewNumber + postDto.Score) / (restaurant.TotalReviewNumber + 1);
        restaurant.TotalReviewNumber++;

        var review = new Review
        {
            Description = postDto.Description,
            Grade = postDto.Score,
            CreatedOn = DateTime.Now,
            OrderId = postDto.OrderId,
            PostedOn = DateTime.Now
        };

        await _unitOfWork.ReviewRepository.AddAsync(review);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(int orderId, OrderStatus status, DateTime? deliveryTime)
    {
        var order = await _unitOfWork.OrderRepository.GetAsync(orderId);

        order.Status = status;
        order.DeliveryTime = deliveryTime;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PageResultDto<OrderGetListDto>> GetAllForRestaurantAsync(string searchText, int page)
    {
        var restaurantId = _userSessionProvider.GetRestaurantId();

        var orders = await _unitOfWork.OrderRepository.GetAsync(restaurantId, searchText, page);
        var count = await _unitOfWork.OrderRepository.GetCountAsync(restaurantId, searchText);

        var ordersDto = orders.Select(x => new OrderGetListDto
        {
            OrderId = x.OrderId,
            TotalPrice = x.Total,
            CreatedOn = x.CreatedOn,
            Card = x.Card,
            Address = x.Address,
            Status = x.Status,
            UserName = x.User.Name,
            Phone = x.User.Phone,
            DeliveryTime = x.DeliveryTime,
            Products = x.OrderProducts.Select(y => new OrderProductGetListDto
            {
                Discount = y.DiscountPercentage,
                Price = y.PricePerQuantity,
                Quantity = y.Quantity,
                Name = y.Name
            }).ToList()
        }).ToList();

        return new PageResultDto<OrderGetListDto>
        {
            Entities = ordersDto,
            NumberOfRecords = count
        };
    }

    public async Task<List<OrderGetListDto>> GetAllForUserAsync()
    {
        var userId = _userSessionProvider.GetUserId();

        var orders = await _unitOfWork.OrderRepository.GetAllForUserAsync(userId);

        return orders.Select(x => new OrderGetListDto
        {
            OrderId = x.OrderId,
            TotalPrice = x.Total,
            RestaurantName = x.Restaurant.Name,
            RestaurantId = x.RestaurantId,
            CreatedOn = x.CreatedOn,
            Card = x.Card,
            Address = x.Address,
            Status = x.Status,
            Phone = x.User?.Phone,
            DeliveryTime = x.DeliveryTime,
            Review = new ReviewGetListDto
            {
                Grade = x.Reviews.FirstOrDefault()?.Grade ?? -1,
                Description = x.Reviews.FirstOrDefault()?.Description
            },
            Products = x.OrderProducts.Select(y => new OrderProductGetListDto
            {
                Discount = y.DiscountPercentage,
                Price = y.PricePerQuantity,
                Quantity = y.Quantity,
                Image = y.Image,
                Name = y.Name
            }).ToList()
        }).ToList();
    }

    public async Task<OrderDashboardGetDto> GetDashboardAsync(OrderDashboardType orderDashboardType)
    {
        var restaurantId = _userSessionProvider.GetRestaurantId();

        var provider = _orderDashboardFactory.Create(orderDashboardType);

        return await provider.GetAsync(restaurantId);
    }

    public async Task PlaceAsync(int addressId, int cardId)
    {
        var userId = _userSessionProvider.GetUserId();

        var cartId = _userSessionProvider.GetCartId();
        var cart = await _unitOfWork.CartRepository.GetAsync(cartId);

        var restaurantOpen = await _unitOfWork.ScheduleRepository.GetForRestaurantTodayAsync(cart.RestaurantId!.Value);
        if (restaurantOpen == null)
        {
            throw new Exception("RestaurantClosedException");
        }

        var order = new Order
        {
            UserId = userId,
            RestaurantId = cart.RestaurantId!.Value,
            Status = OrderStatus.New,
            CreatedOn = DateTime.Now,
            OrderProducts = cart.Products.Select(x => new OrderProduct
            {
                DiscountPercentage = x.Discount,
                PricePerQuantity = x.Price,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                CreatedOn = DateTime.Now,
                Name = x.Name,
                Image = x.Image
            }).ToList()
        };

        decimal totalPrice = 0;

        foreach (var product in order.OrderProducts)
        {
            var productTotalPrice = product.Quantity * product.PricePerQuantity;

            if (product.DiscountPercentage.HasValue)
            {
                totalPrice += (productTotalPrice - (product.DiscountPercentage.Value / 100 * productTotalPrice));
            }
            else
            {
                totalPrice += productTotalPrice;
            }
        }

        order.Total = totalPrice;

        var address = await _unitOfWork.UserAddressRepository.GetAsync(addressId);
        order.Address = $"{address.Address}, Str. {address.Street}, {address.City}";

        var card = await _unitOfWork.UserCardRepository.GetAsync(cardId);
        order.Card = $"{card.Owner} - {card.CardNumber}";

        cart.Products = new List<CartProduct>();
        cart.RestaurantId = null;
        cart.TotalPrice = 0;

        await _unitOfWork.OrderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();
    }
}
