using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Carts;

namespace FoodRestaurant.Abstractions.Aggregates.Carts;

public interface ICartRepository : IBaseRepository<Cart>
{
}
