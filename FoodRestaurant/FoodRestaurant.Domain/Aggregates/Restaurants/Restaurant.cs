using FoodRestaurant.Domain.Aggregates.Categories;
using FoodRestaurant.Domain.Aggregates.Schedules;
using FoodRestaurant.Domain.BaseEntities;

namespace FoodRestaurant.Domain.Aggregates.Restaurants;

public class Restaurant : TEntity
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Delivery { get; set; }
    public decimal? Transport { get; set; }
    public bool? IsValidated { get; set; }
    public decimal AverageGrade { get; set; }
    public int TotalReviewNumber { get; set; }

    public List<Category> Categories { get; set; }
    public List<Schedule> Schedule { get; set; }
}
