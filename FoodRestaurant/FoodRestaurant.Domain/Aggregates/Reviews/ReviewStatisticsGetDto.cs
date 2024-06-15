namespace FoodRestaurant.Domain.Aggregates.Reviews;

public class ReviewStatisticsGetDto
{
    public decimal AverageGrade { get; set; }
    public int TotalReviewNumber { get; set; }
}
