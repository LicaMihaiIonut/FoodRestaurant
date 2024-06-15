using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Users;

namespace FoodRestaurant.Abstractions.Aggregates.Users;

public interface IUserCardRepository : IBaseRepository<UserCard>
{
    Task<List<UserCard>> GetAllByUserIdAsync(int userId);
}
