namespace FoodRestaurant.Domain.Aggregates.Carts;

public class CartGetDto
{
    public decimal TotalPrice { get; set; }
    public decimal? Transport { get; set; }
    public List<CartProductGetDto> Products { get; set; }
}
