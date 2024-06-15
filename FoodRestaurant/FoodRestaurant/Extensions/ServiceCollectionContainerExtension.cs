using FoodRestaurant.Abstractions.Aggregates.Carts;
using FoodRestaurant.Abstractions.Aggregates.Categories;
using FoodRestaurant.Abstractions.Aggregates.Orders;
using FoodRestaurant.Abstractions.Aggregates.RestaurantMenus;
using FoodRestaurant.Abstractions.Aggregates.Restaurants;
using FoodRestaurant.Abstractions.Aggregates.Reviews;
using FoodRestaurant.Abstractions.Aggregates.Schedules;
using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Logic.Aggregates.Carts;
using FoodRestaurant.Logic.Aggregates.Categories;
using FoodRestaurant.Logic.Aggregates.Orders;
using FoodRestaurant.Logic.Aggregates.Orders.Providers;
using FoodRestaurant.Logic.Aggregates.RestaurantMenus;
using FoodRestaurant.Logic.Aggregates.Restaurants;
using FoodRestaurant.Logic.Aggregates.Reviews;
using FoodRestaurant.Logic.Aggregates.Schedules;
using FoodRestaurant.Logic.Aggregates.Users;
using FoodRestaurant.Repositories.Context;

namespace FoodRestaurant.Extensions;

public static class ServiceCollectionContainerExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserTokenProvider, UserTokenProvider>();
        services.AddScoped<IUserLogic, UserLogic>();
        services.AddScoped<IUserCardLogic, UserCardLogic>();
        services.AddScoped<IUserAddressLogic, UserAddressLogic>();
        services.AddScoped<IRestaurantLogic, RestaurantLogic>();
        services.AddScoped<IRestaurantMenuLogic, RestaurantMenuLogic>();
        services.AddScoped<ICategoryLogic, CategoryLogic>();
        services.AddScoped<IScheduleLogic, ScheduleLogic>();
        services.AddScoped<IReviewLogic, ReviewLogic>();
        services.AddScoped<ICartLogic, CartLogic>();

        services.AddScoped<IOrderDashboardFactory, OrderDashboardFactory>();
        services.AddScoped<IOrderLogic, OrderLogic>();
        services.AddScoped<IOrderDashboardProvider, OrderDashboardMonthProvider>();
        services.AddScoped<IOrderDashboardProvider, OrderDashboardQuarterProvider>();
        services.AddScoped<IOrderDashboardProvider, OrderDashboardYearlyProvider>();

        services.AddScoped<IUserSessionProvider, UserSessionProvider>();
    }
}
