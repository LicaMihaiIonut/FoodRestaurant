namespace FoodRestaurant.Domain.Aggregates.RestaurantMenus;

public class RestaurantMenuPostDto
{
    public int? RestaurantMenuId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public DateTime? DiscountUntil { get; set; }
    public int CategoryId { get; set; }
}
