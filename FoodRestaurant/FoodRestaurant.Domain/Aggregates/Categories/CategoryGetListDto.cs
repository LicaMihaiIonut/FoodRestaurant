using FoodRestaurant.Domain.Aggregates.RestaurantMenus;

namespace FoodRestaurant.Domain.Aggregates.Categories;

public class CategoryGetListDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public decimal? Discount { get; set; }
    public DateTime? DiscountUntil { get; set; }
    public bool IsAvailable { get; set; }
    public int NumberOfProducts { get; set; }

    public List<RestaurantMenuGetListDto> Menu { get; set; }
}
