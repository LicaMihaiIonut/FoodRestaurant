using FoodRestaurant.Abstractions.Aggregates.RestaurantMenus;
using FoodRestaurant.Domain.Aggregates.RestaurantMenus;
using FoodRestaurant.Domain.Context;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Repositories.Aggregates.RestaurantMenus;

public class RestaurantMenuRepository : BaseRepository<RestaurantMenu>, IRestaurantMenuRepository
{
    public RestaurantMenuRepository(FoodRestaurantContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> GetCountAsync(int restaurantId, string searchText)
    {
        var query = _dbContext.RestaurantMenus.Where(x => x.Category.RestaurantId == restaurantId).AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(x => x.Name.Contains(searchText) || x.Price.ToString().Contains(searchText));
        }

        return await query.CountAsync();
    }

    public async Task<IEnumerable<RestaurantMenu>> GetAsync(int restaurantId, string searchText, int? currentPage)
    {
        var query = _dbContext.RestaurantMenus.Include(x => x.Category).Where(x => x.Category.RestaurantId == restaurantId).AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(x => x.Name.Contains(searchText) || x.Price.ToString().Contains(searchText));
        }

        if (currentPage.HasValue)
        {
            query = query.OrderBy(x => x.RestaurantMenuId).Skip((currentPage.Value - 1) * 10).Take(10);
        }

        return await query.ToListAsync();
    }
}
