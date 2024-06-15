namespace FoodRestaurant.Domain.Aggregates.Orders;

public class OrderReviewPostDto
{
    public int OrderId { get; set; }
    public int Score { get; set; }
    public string Description { get; set; }
}
