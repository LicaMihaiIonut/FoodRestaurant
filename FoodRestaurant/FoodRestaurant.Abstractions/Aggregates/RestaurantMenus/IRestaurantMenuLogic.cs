using FoodRestaurant.Domain.Aggregates.RestaurantMenus;
using FoodRestaurant.Domain.Pages;

namespace FoodRestaurant.Abstractions.Aggregates.RestaurantMenus;

public interface IRestaurantMenuLogic
{
    Task<PageResultDto<RestaurantMenuGetListDto>> GetAsync(string searchText, int? currentPage);
    Task PostAsync(RestaurantMenuPostDto postDto);
    Task UploadImageAsync(RestaurantMenuUploadImagePostDto postDto);
    Task DeleteAsync(List<int> restaurantMenuIds);
}
