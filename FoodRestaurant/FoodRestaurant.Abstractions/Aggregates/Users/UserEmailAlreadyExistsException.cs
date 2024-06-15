namespace FoodRestaurant.Abstractions.Aggregates.Users;

public class UserEmailAlreadyExistsException : Exception
{
    public UserEmailAlreadyExistsException(string userEmail) : base($"Email {userEmail} is used.")
    {
    }
}
