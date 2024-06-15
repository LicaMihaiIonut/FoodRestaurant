using FoodRestaurant.Abstractions.Aggregates.Categories;
using FoodRestaurant.Domain.Aggregates.Categories;
using FoodRestaurant.Domain.Context;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Repositories.Aggregates.Categories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(FoodRestaurantContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Category>> GetAsync(int restaurantId, string searchText, int? currentPage)
    {
        var query = _dbContext.Categories.Include(x => x.RestaurantMenus).Where(x => x.RestaurantId == restaurantId).AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(x => x.Name.Contains(searchText));
        }

        if (currentPage.HasValue)
        {
            query = query.OrderBy(x => x.CategoryId).Skip((currentPage.Value - 1) * 10).Take(10);
        }
        else
        {
            query = query.OrderBy(x => x.Name);
        }

        return await query.ToListAsync();
    }

    public async Task<int> GetCountAsync(int restaurantId, string searchText)
    {
        var query = _dbContext.Categories.Where(x => x.RestaurantId == restaurantId).AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(x => x.Name.Contains(searchText));
        }

        return await query.CountAsync();
    }
}
