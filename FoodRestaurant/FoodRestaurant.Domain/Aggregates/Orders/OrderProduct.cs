using FoodRestaurant.Domain.Aggregates.Products;
using FoodRestaurant.Domain.BaseEntities;

namespace FoodRestaurant.Domain.Aggregates.Orders;

public class OrderProduct : TEntity
{
    public int OrderProductId { get; set; }

    public int ProductId { get; set; }
    public int OrderId { get; set; }

    public int Quantity { get; set; }
    public decimal PricePerQuantity { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }
}
