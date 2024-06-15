namespace FoodRestaurant.Domain.Aggregates.Schedules;

public class ScheduleGetListDto
{
    public int ScheduleId { get; set; }

    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public bool IsFreeDay { get; set; }
}
