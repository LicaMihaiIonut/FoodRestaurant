using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Products;

namespace FoodRestaurant.Abstractions.Aggregates.Products;

public interface IProductRepository : IBaseRepository<Product>
{
}
