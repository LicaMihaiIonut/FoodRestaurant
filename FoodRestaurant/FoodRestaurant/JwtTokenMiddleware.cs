using FoodRestaurant.Abstractions.Aggregates.Users;

namespace FoodRestaurant;

public class JwtTokenMiddleware : IMiddleware
{
    private readonly IUserSessionProvider _userSessionProvider;

    public JwtTokenMiddleware(IUserSessionProvider userSessionProvider)
    {
        _userSessionProvider = userSessionProvider;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var restaurantId = context.Request.Headers["RestaurantId"].FirstOrDefault()?.Split(" ").LastOrDefault();
        var userId = context.Request.Headers["UserId"].FirstOrDefault()?.Split(" ").LastOrDefault();
        var cartId = context.Request.Headers["CartId"].FirstOrDefault()?.Split(" ").LastOrDefault();

        _ = int.TryParse(restaurantId, out var restaurantIdInt);
        _ = int.TryParse(userId, out var userIdInt);
        _ = int.TryParse(cartId, out var cartIdInt);

        _userSessionProvider.SetSession(restaurantIdInt, userIdInt, cartIdInt);

        await next(context);
    }
}
