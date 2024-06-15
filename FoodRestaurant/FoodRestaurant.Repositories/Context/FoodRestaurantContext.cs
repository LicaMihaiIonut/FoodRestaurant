using FoodRestaurant.Domain.Aggregates.Carts;
using FoodRestaurant.Domain.Aggregates.Categories;
using FoodRestaurant.Domain.Aggregates.Orders;
using FoodRestaurant.Domain.Aggregates.RestaurantMenus;
using FoodRestaurant.Domain.Aggregates.Restaurants;
using FoodRestaurant.Domain.Aggregates.Reviews;
using FoodRestaurant.Domain.Aggregates.Schedules;
using FoodRestaurant.Domain.Aggregates.Users;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Domain.Context;

public class FoodRestaurantContext : DbContext
{
    public FoodRestaurantContext(DbContextOptions<FoodRestaurantContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserCard> UserCards { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<RestaurantMenu> RestaurantMenus { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrdersProducts { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }
}
