namespace FoodRestaurant.Domain.Aggregates.Orders;

public class OrderProductGetListDto
{
    public string Name { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public int Quantity { get; set; }
}
