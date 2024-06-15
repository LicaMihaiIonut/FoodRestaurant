using FoodRestaurant.Abstractions.Aggregates.Carts;
using FoodRestaurant.Domain.Aggregates.Carts;
using FoodRestaurant.Domain.Context;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Repositories.Aggregates.Carts;

public class CartRepository : BaseRepository<Cart>, ICartRepository
{
    public CartRepository(FoodRestaurantContext dbContext) : base(dbContext)
    {
    }

    public override Task<Cart> GetAsync(int id)
    {
        return _dbContext.Carts.Include(x => x.Products).Where(x => x.CartId == id).FirstOrDefaultAsync();
    }
}
