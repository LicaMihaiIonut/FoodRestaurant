using FoodRestaurant.Abstractions.Aggregates.Carts;
using FoodRestaurant.Abstractions.Aggregates.Products;
using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Carts;

namespace FoodRestaurant.Logic.Aggregates.Carts;

public class CartLogic : ICartLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSessionProvider _userSessionProvider;

    public CartLogic(IUnitOfWork unitOfWork, IUserSessionProvider userSessionProvider)
    {
        _unitOfWork = unitOfWork;
        _userSessionProvider = userSessionProvider;
    }

    public async Task<CartProductCountDto> AddOrUpdateProductAsync(CartPostDto postDto)
    {
        var userId = _userSessionProvider.GetCartId();
        var cart = await _unitOfWork.CartRepository.GetAsync(userId);

        if (cart == null)
        {
            throw new Exception("Cart not found.");
        }

        var cartProduct = cart.Products.FirstOrDefault(x => x.ProductId == postDto.ProductId);

        if (cartProduct == null)
        {
            var product = await _unitOfWork.RestaurantMenuRepository.GetAsync(postDto.ProductId);

            cartProduct = new CartProduct
            {
                ProductId = postDto.ProductId,
                Discount = product.Discount,
                Image = product.Image,
                Price = product.Price,
                Quantity = postDto.Quantity,
                Name = product.Name
            };

            cart.Products.Add(cartProduct);

            var restaurantId = await _unitOfWork.RestaurantRepository.GetRestaurantByProductAsync(cartProduct!.ProductId);

            if (cart.RestaurantId == null)
            {
                cart.RestaurantId = restaurantId;
            }
            else
            {
                if (cart.RestaurantId != restaurantId)
                {
                    throw new InvalidProductSelectionException();
                }
            }
        }
        else
        {
            cartProduct.Quantity += postDto.Quantity;
        }

        await _unitOfWork.SaveChangesAsync();

        return new CartProductCountDto
        {
            NumberOfProducts = cart.Products.Count
        };
    }

    public async Task DeleteAsync(int cartProductId)
    {
        var userId = _userSessionProvider.GetCartId();
        var cart = await _unitOfWork.CartRepository.GetAsync(userId);

        if (cart == null)
        {
            throw new Exception("Cart not found.");
        }

        var cartProduct = cart.Products.FirstOrDefault(x => x.CartProductId == cartProductId);

        if (cartProduct != null)
        {
            cart.Products.Remove(cartProduct);

            if (cart.Products.Count == 0)
            {
                cart.RestaurantId = null;
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<CartGetDto> GetAsync()
    {
        var userId = _userSessionProvider.GetCartId();
        var cart = await _unitOfWork.CartRepository.GetAsync(userId);

        if (cart == null)
        {
            throw new Exception("Cart not found.");
        }

        var cartDto = new CartGetDto
        {
            TotalPrice = cart.TotalPrice,
            Products = cart.Products.Select(x => new CartProductGetDto
            {
                CartProductId = x.CartProductId,
                ProductId = x.ProductId,
                Description = x.Description,
                Discount = x.Discount,
                Image = x.Image,
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity,
            }).ToList()
        };

        if (cart.RestaurantId.HasValue)
        {
            var restaurant = await _unitOfWork.RestaurantRepository.GetRestaurantAsync(cart.RestaurantId.Value);

            cartDto.Transport = restaurant.Transport;
        }

        return cartDto;
    }
}
