using FoodRestaurant.Abstractions.Aggregates.Carts;
using FoodRestaurant.Domain.Aggregates.Carts;

using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurant.Controllers;

[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartLogic _cartLogic;

    public CartController(ICartLogic cartLogic)
    {
        _cartLogic = cartLogic;
    }

    [HttpGet]
    public async Task<CartGetDto> Get()
    {
        return await _cartLogic.GetAsync();
    }

    [HttpPost("post")]
    public async Task<CartProductCountDto> Post([FromBody] CartPostDto postDto)
    {
        return await _cartLogic.AddOrUpdateProductAsync(postDto);
    }

    [HttpPost("patch")]
    public async Task<CartProductCountDto> Patch([FromBody] CartPostDto postDto)
    {
        return await _cartLogic.AddOrUpdateProductAsync(postDto);
    }

    [HttpDelete("delete/{cartProductId}")]
    public async Task Delete(int cartProductId)
    {
        await _cartLogic.DeleteAsync(cartProductId);
    }
}
