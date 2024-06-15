using FoodRestaurant.Domain.Aggregates.Orders;
using FoodRestaurant.Domain.BaseEntities;

namespace FoodRestaurant.Domain.Aggregates.Reviews;

public class Review : TEntity
{
    public int ReviewId { get; set; }
    public int OrderId { get; set; }
    public int Grade { get; set; }
    public string Description { get; set; }
    public DateTime PostedOn { get; set; }

    public Order Order { get; set; }
}
