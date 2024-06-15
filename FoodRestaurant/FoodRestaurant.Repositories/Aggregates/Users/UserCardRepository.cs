using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Domain.Aggregates.Users;
using FoodRestaurant.Domain.Context;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Repositories.Aggregates.Users;

public class UserCardRepository : BaseRepository<UserCard>, IUserCardRepository
{
    public UserCardRepository(FoodRestaurantContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<UserCard>> GetAllByUserIdAsync(int userId)
    {
        return await _dbContext.UserCards.Where(x => x.UserId == userId).ToListAsync();
    }
}
