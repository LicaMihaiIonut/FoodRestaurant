using FoodRestaurant.Domain.Aggregates.Carts;

namespace FoodRestaurant.Abstractions.Aggregates.Carts;

public interface ICartLogic
{
    Task<CartProductCountDto> AddOrUpdateProductAsync(CartPostDto postDto);
    Task<CartGetDto> GetAsync();
    Task DeleteAsync(int cartProductId);
}
