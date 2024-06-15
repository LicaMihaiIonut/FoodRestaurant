using FoodRestaurant.Domain.Aggregates.Orders;

namespace FoodRestaurant.Abstractions.Aggregates.Orders;

public interface IOrderDashboardFactory
{
    IOrderDashboardProvider Create(OrderDashboardType orderDashboardType);
}
