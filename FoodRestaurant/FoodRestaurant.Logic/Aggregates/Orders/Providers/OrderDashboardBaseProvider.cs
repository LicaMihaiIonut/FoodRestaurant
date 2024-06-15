using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Orders;
using FoodRestaurant.Domain.Aggregates.Reviews;

namespace FoodRestaurant.Logic.Aggregates.Orders.Providers;

public abstract class OrderDashboardBaseProvider
{
    protected readonly IUnitOfWork _unitOfWork;

    protected OrderDashboardBaseProvider(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDashboardGetDto> GetAsync(int restaurantId)
    {
        var orders = await GetOrdersAsync(restaurantId);

        var result = new OrderDashboardGetDto();

        result = FillRevenue(result, orders);
        result = FillOrder(result, orders);
        result = FillAverage(result, orders);
        result = FillData(result, orders);

        result = await FillReviewsAsync(result, restaurantId);

        return result;
    }

    private OrderDashboardGetDto FillRevenue(OrderDashboardGetDto dto, List<OrderDashboardModel> orders)
    {
        var currentMonthRevenue = GetOrdersForDate(orders, GetCurrent())?.Sum(x => x.Total) ?? 0;
        var previousMonthRevenue = GetOrdersForDate(orders, GetPrevious())?.Sum(x => x.Total) ?? 0;

        dto.Revenue = currentMonthRevenue;
        dto.RevenuePercentage = OrderHelper.CalculatePercentage(currentMonthRevenue, previousMonthRevenue);

        return dto;
    }

    private OrderDashboardGetDto FillAverage(OrderDashboardGetDto dto, List<OrderDashboardModel> orders)
    {
        var previousMonthRevenue = GetOrdersForDate(orders, GetCurrent())?.Sum(x => x.Total) ?? 0;
        var previousMonthOrderCount = GetOrdersForDate(orders, GetPrevious())?.Count ?? 0;
        var previousMonth = previousMonthOrderCount != 0 ? previousMonthRevenue / previousMonthOrderCount : 0;

        dto.AverageRevenue = dto.OrderCount != 0 ? dto.Revenue / dto.OrderCount : 0;
        dto.AverageRevenuePercentage = OrderHelper.CalculatePercentage(dto.AverageRevenue, previousMonth);

        return dto;
    }

    private OrderDashboardGetDto FillOrder(OrderDashboardGetDto dto, List<OrderDashboardModel> orders)
    {
        var currentMonthOrderCount = GetOrdersForDate(orders, GetCurrent())?.Count ?? 0;
        var previousMonthOrderCount = GetOrdersForDate(orders, GetPrevious())?.Count ?? 0;

        dto.OrderCount = currentMonthOrderCount;
        dto.OrderCountPercentage = OrderHelper.CalculatePercentage(currentMonthOrderCount, previousMonthOrderCount);

        return dto;
    }

    private OrderDashboardGetDto FillData(OrderDashboardGetDto dto, List<OrderDashboardModel> orders)
    {
        dto.Data = new List<OrderDashboardGraphGetDto>();

        var firstDate = orders.OrderBy(x => x.CreatedOn).FirstOrDefault()?.CreatedOn;
        if (firstDate == null)
        {
            return dto;
        }

        while (1 != 0)
        {
            var filterDates = GetFilterDates(firstDate.Value);

            if (filterDates.Any(x => x.Year > DateTime.Today.Year))
            {
                break;
            }

            if (filterDates.All(x => x.Year == DateTime.Today.Year && x.Month > DateTime.Today.Month))
            {
                break;
            }

            var monthRevenue = GetOrdersForDate(orders, filterDates)?.Sum(x => x.Total) ?? 0;

            dto.Data.Add(new OrderDashboardGraphGetDto
            {
                ColumnName = GetColumnName(filterDates),
                Value = monthRevenue
            });

            firstDate = firstDate.Value.AddMonths(GetMonthIncrement());
        }

        return dto;
    }


    private async Task<OrderDashboardGetDto> FillReviewsAsync(OrderDashboardGetDto dto, int restaurantId)
    {
        var reviews = await GetReviewsAsync(restaurantId);

        var currentMonthReviews = GetReviewsForDate(reviews, GetCurrent())?.Average(x => x.Grade) ?? 0;
        var previousMonthReviews = GetReviewsForDate(reviews, GetPrevious())?.Average(x => x.Grade) ?? 0;

        dto.Reviews = currentMonthReviews;
        dto.ReviewsPercentage = OrderHelper.CalculatePercentage(currentMonthReviews, previousMonthReviews);

        return dto;
    }

    protected List<OrderDashboardModel>? GetOrdersForDate(List<OrderDashboardModel> orders, List<DateTime> dateTimes)
    {
        var list = new List<OrderDashboardModel>();

        foreach (var dateTime in dateTimes)
        {
            var orderList = orders.Where(x => x.CreatedOn.Year == dateTime.Year && x.CreatedOn.Month == dateTime.Month);

            if (orderList != null)
            {
                list.AddRange(orderList);
            }
        }

        return list.Count != 0 ? list : null;
    }

    protected List<ReviewDashboardModel>? GetReviewsForDate(List<ReviewDashboardModel> reviews, List<DateTime> dateTimes)
    {
        var list = new List<ReviewDashboardModel>();

        foreach (var dateTime in dateTimes)
        {
            var reviewList = reviews.Where(x => x.CreatedOn.Year == dateTime.Year && x.CreatedOn.Month == dateTime.Month);

            if (reviewList != null)
            {
                list.AddRange(reviewList);
            }
        }

        return list.Count != 0 ? list : null;
    }

    protected async Task<List<OrderDashboardModel>> GetOrdersAsync(int restaurantId)
    {
        return await _unitOfWork.OrderRepository.GetPricesPerMonthAsync(restaurantId);
    }

    protected async Task<List<ReviewDashboardModel>> GetReviewsAsync(int restaurantId)
    {
        return await _unitOfWork.ReviewRepository.GetReviewsPerMonthAsync(restaurantId);
    }
    protected abstract List<DateTime> GetCurrent(DateTime? dateTime = null);
    protected abstract List<DateTime> GetPrevious();
    protected abstract List<DateTime> GetFilterDates(DateTime dateTime);
    protected abstract int GetMonthIncrement();
    protected abstract string GetColumnName(List<DateTime> dates);
}
