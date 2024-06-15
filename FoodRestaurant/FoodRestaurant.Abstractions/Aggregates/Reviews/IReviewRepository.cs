using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Reviews;

namespace FoodRestaurant.Abstractions.Aggregates.Reviews;

public interface IReviewRepository : IBaseRepository<Review>
{
    Task<int> GetCountAsync(int restaurantId, string searchText);
    Task<IEnumerable<Review>> GetAsync(int restaurantId, string searchText, int? currentPage);
    Task<List<int>> GetAllGradesAsync(int restaurantId);
    Task<List<ReviewDashboardModel>> GetReviewsPerMonthAsync(long? restaurantId);
}
