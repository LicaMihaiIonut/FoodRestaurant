namespace FoodRestaurant.Domain.Aggregates.Users;

public class UserLoginGetDto
{
    public int UserId { get; set; }
    public int? RestaurantId { get; set; }
    public int? CartId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Token { get; set; }
    public int NumberOfProducts { get; set; }
}
