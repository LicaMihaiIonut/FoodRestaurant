using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Users;

namespace FoodRestaurant.Abstractions.Aggregates.Users;

public interface IUserAddressRepository : IBaseRepository<UserAddress>
{
    Task<List<UserAddress>> GetAllForUserAsync(int userId);
}
