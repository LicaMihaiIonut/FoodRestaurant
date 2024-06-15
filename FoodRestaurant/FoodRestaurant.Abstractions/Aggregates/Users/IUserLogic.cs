using FoodRestaurant.Domain.Aggregates.Users;

namespace FoodRestaurant.Abstractions.Aggregates.Users;

public interface IUserLogic
{
    Task<UserLoginGetDto> RegisterAsync(UserRegisterPostDto dto);
    Task<UserLoginGetDto> LoginAsync(UserLoginPostDto dto);
    Task ChangePasswordAsync(UserChangePasswordDto dto);
    Task<UserAccountDetailsGetDto> GetAccountDetailsAsync();
    Task UpdateAccountDetailsAsync(UserAccountDetailsPostDto dto);
}
