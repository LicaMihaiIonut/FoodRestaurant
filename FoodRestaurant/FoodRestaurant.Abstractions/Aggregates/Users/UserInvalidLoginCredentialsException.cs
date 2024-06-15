namespace FoodRestaurant.Abstractions.Aggregates.Users;

public class UserInvalidLoginCredentialsException : Exception
{
    public UserInvalidLoginCredentialsException() : base("Email or password are invalid.")
    {
    }
}
