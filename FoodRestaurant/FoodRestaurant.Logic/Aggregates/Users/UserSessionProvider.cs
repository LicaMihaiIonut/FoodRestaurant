using FoodRestaurant.Abstractions.Aggregates.Users;

namespace FoodRestaurant.Logic.Aggregates.Users;

public class UserSessionProvider : IUserSessionProvider
{
    private int _restaurantId;
    private int _userId;
    private int _cartId;

    public void SetSession(int restaurantId, int userId, int cartId)
    {
        _restaurantId = restaurantId;
        _userId = userId;
        _cartId = cartId;
    }

    public int GetRestaurantId()
    {
        return _restaurantId;
    }

    public int GetUserId()
    {
        return _userId;
    }

    public int GetCartId()
    {
        return _cartId;
    }
}
