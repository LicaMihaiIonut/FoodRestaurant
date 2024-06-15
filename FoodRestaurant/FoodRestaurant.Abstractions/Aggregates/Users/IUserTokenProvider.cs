using FoodRestaurant.Domain.Aggregates.Users;

namespace FoodRestaurant.Abstractions.Aggregates.Users;

public interface IUserTokenProvider
{
    string Create(User user);
    int? Validate(string token);
}
