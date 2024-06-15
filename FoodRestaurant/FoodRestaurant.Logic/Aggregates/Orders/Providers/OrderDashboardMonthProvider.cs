using FoodRestaurant.Abstractions.Aggregates.Orders;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Orders;

namespace FoodRestaurant.Logic.Aggregates.Orders.Providers;

public class OrderDashboardMonthProvider : OrderDashboardBaseProvider, IOrderDashboardProvider
{
    public OrderDashboardMonthProvider(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override List<DateTime> GetCurrent(DateTime? dateTime = null)
    {
        return new List<DateTime> { dateTime ?? DateTime.Today };
    }

    protected override List<DateTime> GetPrevious()
    {
        return GetCurrent(DateTime.Today.AddMonths(-1));
    }

    protected override List<DateTime> GetFilterDates(DateTime dateTime)
    {
        return GetCurrent(dateTime);
    }

    protected override int GetMonthIncrement()
    {
        return 1;
    }

    protected override string GetColumnName(List<DateTime> dates)
    {
        var date = dates.FirstOrDefault();

        return $"{date.ToString("MMM")} ({date.Year})";
    }

    public OrderDashboardType OrderDashboardType => OrderDashboardType.Month;
}
