using FoodRestaurant.Domain.Aggregates.RestaurantMenus;
using FoodRestaurant.Domain.Aggregates.Restaurants;
using FoodRestaurant.Domain.BaseEntities;

namespace FoodRestaurant.Domain.Aggregates.Categories;

public class Category : TEntity
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public decimal? Discount { get; set; }
    public DateTime? DiscountUntil { get; set; }
    public bool IsAvailable { get; set; }

    public int RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; }
    public List<RestaurantMenu> RestaurantMenus { get; set; }
}
