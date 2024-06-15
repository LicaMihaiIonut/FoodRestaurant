using FoodRestaurant.Abstractions.Aggregates.Carts;
using FoodRestaurant.Abstractions.Aggregates.Categories;
using FoodRestaurant.Abstractions.Aggregates.Orders;
using FoodRestaurant.Abstractions.Aggregates.Products;
using FoodRestaurant.Abstractions.Aggregates.RestaurantMenus;
using FoodRestaurant.Abstractions.Aggregates.Restaurants;
using FoodRestaurant.Abstractions.Aggregates.Reviews;
using FoodRestaurant.Abstractions.Aggregates.Schedules;
using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Context;
using FoodRestaurant.Repositories.Aggregates.Carts;
using FoodRestaurant.Repositories.Aggregates.Categories;
using FoodRestaurant.Repositories.Aggregates.Orders;
using FoodRestaurant.Repositories.Aggregates.Products;
using FoodRestaurant.Repositories.Aggregates.RestaurantMenus;
using FoodRestaurant.Repositories.Aggregates.Restaurants;
using FoodRestaurant.Repositories.Aggregates.Reviews;
using FoodRestaurant.Repositories.Aggregates.Schedules;
using FoodRestaurant.Repositories.Aggregates.Users;

namespace FoodRestaurant.Repositories.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly FoodRestaurantContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IUserCardRepository _userCardRepository;
    private readonly IUserAddressRepository _userAddressRepository;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IRestaurantMenuRepository _restaurantMenuRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;

    public UnitOfWork(FoodRestaurantContext dbContext)
    {
        _dbContext = dbContext;
        _userRepository = new UserRepository(dbContext);
        _userCardRepository = new UserCardRepository(dbContext);
        _userAddressRepository = new UserAddressRepository(dbContext);
        _restaurantRepository = new RestaurantRepository(dbContext);
        _restaurantMenuRepository = new RestaurantMenuRepository(dbContext);
        _productRepository = new ProductRepository(dbContext);
        _categoryRepository = new CategoryRepository(dbContext);
        _scheduleRepository = new ScheduleRepository(dbContext);
        _reviewRepository = new ReviewRepository(dbContext);
        _orderRepository = new OrderRepository(dbContext);
        _cartRepository = new CartRepository(dbContext);
    }

    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_dbContext);
    public IUserCardRepository UserCardRepository => _userCardRepository ?? new UserCardRepository(_dbContext);
    public IUserAddressRepository UserAddressRepository => _userAddressRepository ?? new UserAddressRepository(_dbContext);
    public IRestaurantRepository RestaurantRepository => _restaurantRepository ?? new RestaurantRepository(_dbContext);
    public IRestaurantMenuRepository RestaurantMenuRepository => _restaurantMenuRepository ?? new RestaurantMenuRepository(_dbContext);
    public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_dbContext);
    public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_dbContext);
    public IScheduleRepository ScheduleRepository => _scheduleRepository ?? new ScheduleRepository(_dbContext);
    public IReviewRepository ReviewRepository => _reviewRepository ?? new ReviewRepository(_dbContext);
    public IOrderRepository OrderRepository => _orderRepository ?? new OrderRepository(_dbContext);
    public ICartRepository CartRepository => _cartRepository ?? new CartRepository(_dbContext);

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
