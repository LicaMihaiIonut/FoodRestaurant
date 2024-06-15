
using FoodRestaurant.Abstractions.Aggregates.Orders;
using FoodRestaurant.Domain.Aggregates.Orders;
using FoodRestaurant.Domain.Context;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Repositories.Aggregates.Orders;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(FoodRestaurantContext dbContext) : base(dbContext)
    {
    }

    public Task<List<Order>> GetAllForUserAsync(int userId)
    {
        return _dbContext.Orders.Where(x => x.UserId == userId)
            .Include(x => x.Restaurant)
            .Include(x => x.OrderProducts)
            .Include(x => x.Reviews)
            .OrderByDescending(x => x.CreatedOn)
            .ToListAsync();
    }

    public async Task<List<OrderDashboardModel>> GetPricesPerMonthAsync(int restaurantId)
    {
        return await _dbContext.Orders.Where(x => x.RestaurantId == restaurantId)
        .Select(x => new OrderDashboardModel
        {
            Total = x.Total,
            CreatedOn = x.CreatedOn
        })
        .ToListAsync();
    }

    public async Task<int> GetCountAsync(int restaurantId, string searchText)
    {
        var query = _dbContext.Orders.Where(x => x.RestaurantId == restaurantId).AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(x => x.OrderId.ToString().Contains(searchText) || x.Total.ToString().Contains(searchText));
        }

        return await query.CountAsync();
    }

    public async Task<IEnumerable<Order>> GetAsync(int restaurantId, string searchText, int? currentPage)
    {
        var query = _dbContext.Orders.Include(x => x.User)
            .Include(x => x.OrderProducts)
            .Where(x => x.RestaurantId == restaurantId)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(x => x.OrderId.ToString().Contains(searchText) || x.Total.ToString().Contains(searchText));
        }

        if (currentPage.HasValue)
        {
            query = query.OrderByDescending(x => x.OrderId).Skip((currentPage.Value - 1) * 10).Take(10);
        }

        return await query.ToListAsync();
    }
}
