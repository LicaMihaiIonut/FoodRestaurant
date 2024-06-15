using FoodRestaurant.Domain.BaseEntities;

namespace FoodRestaurant.Domain.Aggregates.Carts;

public class Cart : TEntity
{
    public int CartId { get; set; }
    public int? RestaurantId { get; set; }
    public decimal TotalPrice { get; set; }

    public List<CartProduct> Products { get; set; }
}
