using FoodRestaurant.Abstractions.Aggregates.Schedules;
using FoodRestaurant.Domain.Aggregates.Schedules;
using FoodRestaurant.Domain.Context;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Repositories.Aggregates.Schedules;

public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
{
    public ScheduleRepository(FoodRestaurantContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Schedule>> GetByMonthAsync(int restaurantId, int month, int year)
    {
        return await _dbContext.Schedules.Where(x => x.RestaurantId == restaurantId)
            .Where(x => x.Start.HasValue && x.Start.Value.Month == month)
            .Where(x => x.Start.Value.Year == year)
            .ToListAsync();
    }

    public Task<Schedule> GetForRestaurantTodayAsync(int restaurantId)
    {
        return _dbContext.Schedules.Where(x => x.RestaurantId == restaurantId)
            .Where(x => x.Start.HasValue && x.Start.Value <= DateTime.Now)
            .Where(x => x.End.HasValue && x.End.Value >= DateTime.Now)
            .FirstOrDefaultAsync();
    }
}
