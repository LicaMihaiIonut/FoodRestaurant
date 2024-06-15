namespace FoodRestaurant.Domain.Aggregates.Users;

public class UserRegisterPostDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public bool IsRestaurant { get; set; }
}
