using FoodRestaurant.Abstractions.Aggregates.Orders;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Orders;

namespace FoodRestaurant.Logic.Aggregates.Orders.Providers;

public class OrderDashboardQuarterProvider : OrderDashboardBaseProvider, IOrderDashboardProvider
{
    public OrderDashboardQuarterProvider(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override string GetColumnName(List<DateTime> dates)
    {
        var lastDate = dates.OrderByDescending(x => x).First();

        return $"Q{lastDate.Month / 3} ({lastDate.Year})";
    }

    protected override List<DateTime> GetCurrent(DateTime? dateTime = null)
    {
        var date = dateTime ?? DateTime.Today;

        if (date.Month % 3 == 1)
        {
            return new List<DateTime> { date, date.AddMonths(1), date.AddMonths(1) };
        }

        if (date.Month % 3 == 2)
        {
            return new List<DateTime> { date.AddMonths(-1), date, date.AddMonths(1) };
        }

        return new List<DateTime> { date.AddMonths(-2), date.AddMonths(-1), date };
    }

    protected override List<DateTime> GetFilterDates(DateTime dateTime)
    {
        return GetCurrent(dateTime);
    }

    protected override int GetMonthIncrement()
    {
        return 3;
    }

    protected override List<DateTime> GetPrevious()
    {
        return GetCurrent(DateTime.Today.AddMonths(-3));
    }

    public OrderDashboardType OrderDashboardType => OrderDashboardType.Quarter;

}
