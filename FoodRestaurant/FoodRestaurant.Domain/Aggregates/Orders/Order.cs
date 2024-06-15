using FoodRestaurant.Domain.Aggregates.Restaurants;
using FoodRestaurant.Domain.Aggregates.Reviews;
using FoodRestaurant.Domain.Aggregates.Users;
using FoodRestaurant.Domain.BaseEntities;

namespace FoodRestaurant.Domain.Aggregates.Orders;

public class Order : TEntity
{
    public int OrderId { get; set; }

    public int UserId { get; set; }
    public int RestaurantId { get; set; }

    public decimal Total { get; set; }

    public string Address { get; set; }
    public string Card { get; set; }

    public OrderStatus Status { get; set; }
    public DateTime? DeliveryTime { get; set; }

    public User User { get; set; }
    public Restaurant Restaurant { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public List<Review> Reviews { get; set; }
}

public enum OrderStatus
{
    New = 1,
    Preparing = 2,
    Delivering = 3,
    Delivered
}