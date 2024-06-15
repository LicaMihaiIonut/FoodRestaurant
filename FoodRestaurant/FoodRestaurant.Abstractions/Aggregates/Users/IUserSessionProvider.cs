namespace FoodRestaurant.Abstractions.Aggregates.Users;

public interface IUserSessionProvider
{
    int GetUserId();
    int GetRestaurantId();
    int GetCartId();
    void SetSession(int restaurantId, int userId, int cartId);
}
