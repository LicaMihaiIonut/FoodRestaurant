using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Orders;

namespace FoodRestaurant.Abstractions.Aggregates.Orders;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<List<OrderDashboardModel>> GetPricesPerMonthAsync(int restaurantId);
    Task<List<Order>> GetAllForUserAsync(int userId);
    Task<IEnumerable<Order>> GetAsync(int restaurantId, string searchText, int? currentPage);
    Task<int> GetCountAsync(int restaurantId, string searchText);
}
