using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Users;

namespace FoodRestaurant.Abstractions.Aggregates.Users;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> ValidCredentials(string email, string password);
    Task<User> GetByIdAsync(int userId);
    Task<User> GetByIdAsync(string email);
    Task<bool> ExistsAsync(string email);
    Task<User> GetUserByCredentials(string email, string password);
}
