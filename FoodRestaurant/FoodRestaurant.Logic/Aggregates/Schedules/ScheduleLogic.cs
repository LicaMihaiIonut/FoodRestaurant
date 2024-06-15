using FoodRestaurant.Abstractions.Aggregates.Schedules;
using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Schedules;

namespace FoodRestaurant.Logic.Aggregates.Schedules;

public class ScheduleLogic : IScheduleLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSessionProvider _userSessionProvider;

    public ScheduleLogic(IUnitOfWork unitOfWork, IUserSessionProvider userSessionProvider)
    {
        _unitOfWork = unitOfWork;
        _userSessionProvider = userSessionProvider;
    }

    public async Task DeleteAsync(int scheduleId)
    {
        var schedule = await _unitOfWork.ScheduleRepository.GetAsync(scheduleId);

        if (schedule == null)
        {
            throw new Exception("Schedule was not found.");
        }

        await _unitOfWork.ScheduleRepository.DeleteAsync(schedule);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<ScheduleGetListDto>> GetAsync(int month, int year)
    {
        var restaurantId = _userSessionProvider.GetRestaurantId();

        var schedules = await _unitOfWork.ScheduleRepository.GetByMonthAsync(restaurantId, month, year);

        return schedules.Select(x => new ScheduleGetListDto
        {
            ScheduleId = x.ScheduleId,
            Start = x.Start,
            End = x.End,
            IsFreeDay = x.IsFreeDay
        }).ToList();
    }

    public async Task PatchAsync(SchedulePatchDto schedulePatchDto)
    {
        var schedule = await _unitOfWork.ScheduleRepository.GetAsync(schedulePatchDto.ScheduleId);

        if (schedule == null)
        {
            throw new Exception("Schedule was not found.");
        }

        var offSet = schedulePatchDto.DateTimeOffSet.GetValueOrDefault();
        schedule.Start = schedulePatchDto.Start.HasValue ? schedulePatchDto.Start.Value.AddMinutes(offSet) : null;
        schedule.End = schedulePatchDto.End.HasValue ? schedulePatchDto.End.Value.AddMinutes(offSet) : null;
        schedule.IsFreeDay = schedulePatchDto.IsFreeDay;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task PostAsync(SchedulePostDto schedulePostDto)
    {
        var restaurantId = _userSessionProvider.GetRestaurantId();

        var offSet = schedulePostDto.DateTimeOffSet.GetValueOrDefault();
        DateTime? start = schedulePostDto.Start.HasValue ? schedulePostDto.Start.Value.AddMinutes(offSet) : null;
        DateTime? end = schedulePostDto.End.HasValue ? schedulePostDto.End.Value.AddMinutes(offSet) : null;

        var schedule = new Schedule
        {
            RestaurantId = restaurantId,
            Start = start,
            End = end,
            IsFreeDay = schedulePostDto.IsFreeDay
        };

        await _unitOfWork.ScheduleRepository.AddAsync(schedule);
        await _unitOfWork.SaveChangesAsync();
    }
}
