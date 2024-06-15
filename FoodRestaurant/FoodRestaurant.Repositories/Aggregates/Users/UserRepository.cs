using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Domain.Aggregates.Users;
using FoodRestaurant.Domain.Context;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Repositories.Aggregates.Users;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(FoodRestaurantContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> ExistsAsync(string email)
    {
        var result = _dbContext.Users.Any(x => x.Email == email);
        return Task.FromResult(result);
    }

    public async Task<User> GetByIdAsync(string email)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public Task<bool> ValidCredentials(string email, string password)
    {
        var result = _dbContext.Users.Any(x => x.Email == email && x.Password == password);
        return Task.FromResult(result);
    }

    public async Task<User> GetUserByCredentials(string email, string password)
    {
        return await _dbContext.Users.Include(x => x.Cart).ThenInclude(x => x.Products).SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
    }
}
