using FoodRestaurant.Abstractions.Aggregates.Reviews;
using FoodRestaurant.Domain.Aggregates.Reviews;
using FoodRestaurant.Domain.Context;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Repositories.Aggregates.Reviews;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    public ReviewRepository(FoodRestaurantContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Review>> GetAsync(int restaurantId, string searchText, int? currentPage)
    {
        var query = _dbContext.Reviews.Include(x => x.Order).ThenInclude(x => x.User).Where(x => x.Order.RestaurantId == restaurantId).AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(x => x.Order.User.Name.Contains(searchText) || x.Description.Contains(searchText));
        }

        if (currentPage.HasValue)
        {
            query = query.OrderBy(x => x.ReviewId).Skip((currentPage.Value - 1) * 10).Take(10);
        }

        return await query.ToListAsync();
    }

    public async Task<int> GetCountAsync(int restaurantId, string searchText)
    {
        var query = _dbContext.Reviews.Where(x => x.Order.RestaurantId == restaurantId).AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(x => x.Order.User.Name.Contains(searchText) || x.Description.Contains(searchText));
        }

        return await query.CountAsync();
    }

    public async Task<List<int>> GetAllGradesAsync(int restaurantId)
    {
        return await _dbContext.Reviews.Where(x => x.Order.RestaurantId == restaurantId).Select(x => x.Grade).ToListAsync();
    }

    public async Task<List<ReviewDashboardModel>> GetReviewsPerMonthAsync(long? restaurantId)
    {
        return await _dbContext.Reviews.Where(x => x.Order.RestaurantId == restaurantId)
         .Select(x => new ReviewDashboardModel
         {
             Grade = x.Grade,
             CreatedOn = x.CreatedOn
         })
        .ToListAsync();
    }
}
