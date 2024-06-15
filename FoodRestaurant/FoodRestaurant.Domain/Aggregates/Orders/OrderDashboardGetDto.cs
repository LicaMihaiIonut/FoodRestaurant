namespace FoodRestaurant.Domain.Aggregates.Orders;

public class OrderDashboardGetDto
{
    public decimal Revenue { get; set; }
    public decimal RevenuePercentage { get; set; }

    public decimal OrderCount { get; set; }
    public decimal OrderCountPercentage { get; set; }

    public decimal AverageRevenue { get; set; }
    public decimal AverageRevenuePercentage { get; set; }

    public decimal Reviews { get; set; }
    public decimal ReviewsPercentage { get; set; }

    public List<OrderDashboardGraphGetDto> Data { get; set; }
}
