using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Restaurants;

namespace FoodRestaurant.Abstractions.Aggregates.Restaurants;

public interface IRestaurantRepository : IBaseRepository<Restaurant>
{
    Task<List<ResturantGetListDto>> GetRestaurantsAsync();
    Task<RestaurantGetDto> GetRestaurantAsync(int id);
    Task<int> GetRestaurantByProductAsync(int productId);
    Task<Restaurant> GetByOrderId(int orderId);
}
