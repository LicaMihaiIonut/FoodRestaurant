namespace FoodRestaurant.Domain.Aggregates.Reviews;

public class ReviewGetListDto
{
    public string UserName { get; set; }
    public int Grade { get; set; }
    public string Description { get; set; }
    public DateTime PostedOn { get; set; }
}
