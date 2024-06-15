namespace FoodRestaurant.Domain.Aggregates.Carts;

public class CartProductGetDto
{
    public int CartProductId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal? Discount { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Name { get; set; }
}
