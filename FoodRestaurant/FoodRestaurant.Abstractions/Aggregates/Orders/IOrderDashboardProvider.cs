using FoodRestaurant.Domain.Aggregates.Orders;

namespace FoodRestaurant.Abstractions.Aggregates.Orders;

public interface IOrderDashboardProvider
{
    Task<OrderDashboardGetDto> GetAsync(int restaurantId);

    OrderDashboardType OrderDashboardType { get; }
}
