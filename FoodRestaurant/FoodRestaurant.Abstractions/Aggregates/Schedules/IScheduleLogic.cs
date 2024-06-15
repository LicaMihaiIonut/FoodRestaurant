using FoodRestaurant.Domain.Aggregates.Schedules;

namespace FoodRestaurant.Abstractions.Aggregates.Schedules;

public interface IScheduleLogic
{
    Task<List<ScheduleGetListDto>> GetAsync(int month, int year);
    Task PostAsync(SchedulePostDto schedulePostDto);
    Task PatchAsync(SchedulePatchDto schedulePatchDto);
    Task DeleteAsync(int scheduleId);
}
