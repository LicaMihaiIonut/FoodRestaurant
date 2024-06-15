using FoodRestaurant.Abstractions.Aggregates.Orders;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Orders;

namespace FoodRestaurant.Logic.Aggregates.Orders.Providers;

public class OrderDashboardYearlyProvider : OrderDashboardBaseProvider, IOrderDashboardProvider
{
    public OrderDashboardYearlyProvider(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override string GetColumnName(List<DateTime> dates)
    {
        var lastDate = dates.OrderByDescending(x => x).First();

        return $"{lastDate.Year}";
    }

    protected override List<DateTime> GetCurrent(DateTime? dateTime = null)
    {
        var date = dateTime ?? DateTime.Today;

        var list = new List<DateTime>();

        for (var i = 1; i <= 12; i++)
        {
            list.Add(new DateTime(date.Year, i, 1));
        }

        return list;
    }

    protected override List<DateTime> GetFilterDates(DateTime dateTime)
    {
        return GetCurrent(dateTime);
    }

    protected override int GetMonthIncrement()
    {
        return 12;
    }

    protected override List<DateTime> GetPrevious()
    {
        return GetCurrent(DateTime.Today.AddYears(-1));
    }

    public OrderDashboardType OrderDashboardType => OrderDashboardType.Year;
}
