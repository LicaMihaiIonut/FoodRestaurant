using FoodRestaurant.Abstractions.Aggregates.Products;
using FoodRestaurant.Domain.Aggregates.Products;
using FoodRestaurant.Domain.Context;

namespace FoodRestaurant.Repositories.Aggregates.Products;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(FoodRestaurantContext dbContext) : base(dbContext)
    {
    }
}
