using FoodRestaurant.Domain.Aggregates.Categories;
using FoodRestaurant.Domain.BaseEntities;

namespace FoodRestaurant.Domain.Aggregates.RestaurantMenus;

public class RestaurantMenu : TEntity
{
    public int RestaurantMenuId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public DateTime? DiscountUntil { get; set; }
    public string Image { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }
}
