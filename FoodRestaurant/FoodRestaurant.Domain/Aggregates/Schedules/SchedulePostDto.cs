namespace FoodRestaurant.Domain.Aggregates.Schedules;

public class SchedulePostDto
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public int? DateTimeOffSet { get; set; }
    public bool IsFreeDay { get; set; }
}
