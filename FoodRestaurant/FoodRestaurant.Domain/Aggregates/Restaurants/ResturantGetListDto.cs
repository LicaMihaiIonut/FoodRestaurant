using FoodRestaurant.Domain.Aggregates.Schedules;

namespace FoodRestaurant.Domain.Aggregates.Restaurants;

public class ResturantGetListDto
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Delivery { get; set; }
    public decimal? Transport { get; set; }
    public decimal AverageGrade { get; set; }
    public int TotalReviewNumber { get; set; }
    public ScheduleGetListDto Schedule { get; set; }
}
