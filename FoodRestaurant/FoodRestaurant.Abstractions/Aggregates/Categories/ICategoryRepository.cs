using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Categories;

namespace FoodRestaurant.Abstractions.Aggregates.Categories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<IEnumerable<Category>> GetAsync(int restaurantId, string searchText, int? currentPage);
    Task<int> GetCountAsync(int restaurantId, string searchText);
}
