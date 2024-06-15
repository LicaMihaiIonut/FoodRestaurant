using FoodRestaurant.Domain.BaseEntities;

namespace FoodRestaurant.Domain.Aggregates.Users;

public class UserAddress : TEntity
{
    public int UserAddressId { get; set; }

    public int UserId { get; set; }

    public string City { get; set; }
    public string Street { get; set; }
    public string Address { get; set; }

    public User User { get; set; }
}
