using FoodRestaurant.Abstractions.Aggregates.RestaurantMenus;
using FoodRestaurant.Domain.Aggregates.RestaurantMenus;
using FoodRestaurant.Domain.Pages;

using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurant.Controllers;

[Route("api/[controller]")]
public class RestaurantMenusController : ControllerBase
{
    private readonly IRestaurantMenuLogic _restaurantMenuLogic;

    public RestaurantMenusController(IRestaurantMenuLogic restaurantMenuLogic)
    {
        _restaurantMenuLogic = restaurantMenuLogic;
    }

    [HttpGet]
    public async Task<PageResultDto<RestaurantMenuGetListDto>> GetAsync(string searchText, int? currentPage)
    {
        return await _restaurantMenuLogic.GetAsync(searchText, currentPage);
    }

    [HttpPost]
    [Route("add")]
    public async Task PostAsync([FromBody] RestaurantMenuPostDto postDto)
    {
        await _restaurantMenuLogic.PostAsync(postDto);
    }

    [HttpPost]
    [Route("delete")]
    public async Task DeleteAsync([FromBody] List<int> restaurantMenuIds)
    {
        await _restaurantMenuLogic.DeleteAsync(restaurantMenuIds);
    }

    [HttpPost]
    [Route("upload-image")]
    public async Task UploadAsync([FromBody] RestaurantMenuUploadImagePostDto postDto)
    {
        await _restaurantMenuLogic.UploadImageAsync(postDto);
    }
}
