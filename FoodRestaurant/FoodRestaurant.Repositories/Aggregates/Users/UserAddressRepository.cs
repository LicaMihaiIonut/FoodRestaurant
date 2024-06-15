using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Domain.Aggregates.Users;
using FoodRestaurant.Domain.Context;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Repositories.Aggregates.Users;

public class UserAddressRepository : BaseRepository<UserAddress>, IUserAddressRepository
{
    public UserAddressRepository(FoodRestaurantContext dbContext) : base(dbContext)
    {
    }

    public Task<List<UserAddress>> GetAllForUserAsync(int userId)
    {
        return _dbContext.UserAddresses.Where(x => x.UserId == userId).ToListAsync();
    }
}
