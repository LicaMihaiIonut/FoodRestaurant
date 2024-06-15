using FoodRestaurant.Abstractions.Aggregates.Restaurants;
using FoodRestaurant.Domain.Aggregates.Restaurants;

using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurant.Controllers;

[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantLogic _restaurantLogic;

    public RestaurantsController(IRestaurantLogic restaurantLogic)
    {
        _restaurantLogic = restaurantLogic;
    }

    [HttpGet]
    public async Task<List<ResturantGetListDto>> GetAsync()
    {
        return await _restaurantLogic.GetAsync();
    }

    [HttpGet("{restaurantId}")]
    public async Task<RestaurantGetDto> GetRestaurantAsync(int restaurantId)
    {
        return await _restaurantLogic.GetAsync(restaurantId);
    }

    [HttpGet("profile")]
    public async Task<RestaurantProfileGetDto> GetProfileAsync()
    {
        return await _restaurantLogic.GetProfileAsync();
    }

    [HttpPost("profile")]
    public async Task UpdateProfileAsync([FromBody] RestaurantProfilePostDto postDto)
    {
        await _restaurantLogic.UpdateProfileAsync(postDto);
    }
}
