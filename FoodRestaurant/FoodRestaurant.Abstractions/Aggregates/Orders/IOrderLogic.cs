using FoodRestaurant.Domain.Aggregates.Orders;
using FoodRestaurant.Domain.Pages;

namespace FoodRestaurant.Abstractions.Aggregates.Orders;

public interface IOrderLogic
{
    Task<OrderDashboardGetDto> GetDashboardAsync(OrderDashboardType orderDashboardType);
    Task PlaceAsync(int addressId, int cardId);
    Task<List<OrderGetListDto>> GetAllForUserAsync();
    Task<PageResultDto<OrderGetListDto>> GetAllForRestaurantAsync(string searchText, int page);
    Task AddReviewAsync(OrderReviewPostDto postDto);
    Task ChangeStatusAsync(int orderId, OrderStatus status, DateTime? deliveryTime);
}
