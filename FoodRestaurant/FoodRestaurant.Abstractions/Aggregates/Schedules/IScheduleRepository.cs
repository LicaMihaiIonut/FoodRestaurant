using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Schedules;

namespace FoodRestaurant.Abstractions.Aggregates.Schedules;

public interface IScheduleRepository : IBaseRepository<Schedule>
{
    Task<List<Schedule>> GetByMonthAsync(int restaurantId, int month, int year);
    Task<Schedule> GetForRestaurantTodayAsync(int restaurantId);
}
