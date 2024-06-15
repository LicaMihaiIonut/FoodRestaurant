using FoodRestaurant.Domain.Aggregates.Restaurants;

namespace FoodRestaurant.Abstractions.Aggregates.Restaurants;

public interface IRestaurantLogic
{
    Task<List<ResturantGetListDto>> GetAsync();
    Task<RestaurantGetDto> GetAsync(int restaurantId);
    Task<RestaurantProfileGetDto> GetProfileAsync();
    Task UpdateProfileAsync(RestaurantProfilePostDto postDto);
}
