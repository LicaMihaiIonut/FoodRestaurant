namespace FoodRestaurant.Domain.Aggregates.Categories;

public class CategoryPostDto
{
    public string Name { get; set; }
    public decimal? Discount { get; set; }
    public DateTime? DiscountUntil { get; set; }
    public bool IsAvailable { get; set; }
}
