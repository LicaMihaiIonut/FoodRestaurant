using FoodRestaurant.Abstractions.Aggregates.Schedules;
using FoodRestaurant.Domain.Aggregates.Schedules;

using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurant.Controllers;

[Route("api/[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly IScheduleLogic _scheduleLogic;

    public SchedulesController(IScheduleLogic scheduleLogic)
    {
        _scheduleLogic = scheduleLogic;
    }

    [HttpGet]
    public async Task<List<ScheduleGetListDto>> GetAsync(int month, int year)
    {
        return await _scheduleLogic.GetAsync(month, year);
    }

    [HttpPost]
    [Route("add")]
    public async Task PostAsync([FromBody] SchedulePostDto postDto)
    {
        await _scheduleLogic.PostAsync(postDto);
    }

    [HttpPatch]
    [Route("patch")]
    public async Task PatchAsync([FromBody] SchedulePatchDto postDto)
    {
        await _scheduleLogic.PatchAsync(postDto);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task DeleteAsync(int scheduleId)
    {
        await _scheduleLogic.DeleteAsync(scheduleId);
    }
}
