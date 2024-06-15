using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.RestaurantMenus;

namespace FoodRestaurant.Abstractions.Aggregates.RestaurantMenus;

public interface IRestaurantMenuRepository : IBaseRepository<RestaurantMenu>
{
    Task<IEnumerable<RestaurantMenu>> GetAsync(int restaurantId, string searchText, int? currentPage);
    Task<int> GetCountAsync(int restaurantId, string searchText);
}
