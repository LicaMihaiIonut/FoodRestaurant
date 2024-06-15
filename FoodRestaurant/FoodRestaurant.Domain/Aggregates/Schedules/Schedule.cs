using FoodRestaurant.Domain.Aggregates.Restaurants;
using FoodRestaurant.Domain.BaseEntities;

namespace FoodRestaurant.Domain.Aggregates.Schedules;

public class Schedule : TEntity
{
    public int ScheduleId { get; set; }

    public int RestaurantId { get; set; }

    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public bool IsFreeDay { get; set; }

    public Restaurant Restaurant { get; set; }
}
