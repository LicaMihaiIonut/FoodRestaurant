using FoodRestaurant.Domain.Aggregates.Carts;
using FoodRestaurant.Domain.Aggregates.Restaurants;
using FoodRestaurant.Domain.BaseEntities;

using System.ComponentModel.DataAnnotations;

namespace FoodRestaurant.Domain.Aggregates.Users;

public class User : TEntity
{
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }

    public int? RestaurantId { get; set; }
    public int? CartId { get; set; }

    public Restaurant Restaurant { get; set; }
    public Cart Cart { get; set; }
    public List<UserCard> UserCards { get; set; }
}
