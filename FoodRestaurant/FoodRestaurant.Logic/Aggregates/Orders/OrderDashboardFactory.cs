using FoodRestaurant.Abstractions.Aggregates.Orders;
using FoodRestaurant.Domain.Aggregates.Orders;

namespace FoodRestaurant.Logic.Aggregates.Orders;

public class OrderDashboardFactory : IOrderDashboardFactory
{
    private readonly IEnumerable<IOrderDashboardProvider> _providers;

    public OrderDashboardFactory(IEnumerable<IOrderDashboardProvider> providers)
    {
        _providers = providers;
    }

    public IOrderDashboardProvider Create(OrderDashboardType orderDashboardType)
    {
        foreach (var provider in _providers)
        {
            if (provider.OrderDashboardType != orderDashboardType)
            {
                continue;
            }

            return provider;
        }

        throw new ArgumentException();
    }
}
