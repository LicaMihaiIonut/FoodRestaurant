namespace FoodRestaurant.Abstractions.Aggregates.Users;

public class UserRegistrationPasswordDoNotMatchException : Exception
{
    public UserRegistrationPasswordDoNotMatchException() : base("Passwords do not match.")
    {
    }
}
