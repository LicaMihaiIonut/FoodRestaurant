using FoodRestaurant.Abstractions.Aggregates.Carts;
using FoodRestaurant.Abstractions.Aggregates.Categories;
using FoodRestaurant.Abstractions.Aggregates.Orders;
using FoodRestaurant.Abstractions.Aggregates.Products;
using FoodRestaurant.Abstractions.Aggregates.RestaurantMenus;
using FoodRestaurant.Abstractions.Aggregates.Restaurants;
using FoodRestaurant.Abstractions.Aggregates.Reviews;
using FoodRestaurant.Abstractions.Aggregates.Schedules;
using FoodRestaurant.Abstractions.Aggregates.Users;

namespace FoodRestaurant.Abstractions.Context;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IUserCardRepository UserCardRepository { get; }
    IUserAddressRepository UserAddressRepository { get; }
    IRestaurantRepository RestaurantRepository { get; }
    IRestaurantMenuRepository RestaurantMenuRepository { get; }
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IScheduleRepository ScheduleRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IOrderRepository OrderRepository { get; }
    ICartRepository CartRepository { get; }

    Task SaveChangesAsync();
}
