using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Carts;
using FoodRestaurant.Domain.Aggregates.Restaurants;
using FoodRestaurant.Domain.Aggregates.Users;

namespace FoodRestaurant.Logic.Aggregates.Users;

public class UserLogic : IUserLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserTokenProvider _userTokenProvider;
    private readonly IUserSessionProvider _userSessionProvider;

    public UserLogic(IUnitOfWork unitOfWork, IUserTokenProvider userTokenProvider, IUserSessionProvider userSessionProvider)
    {
        _unitOfWork = unitOfWork;
        _userTokenProvider = userTokenProvider;
        _userSessionProvider = userSessionProvider;
    }

    public async Task<UserLoginGetDto> LoginAsync(UserLoginPostDto dto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByCredentials(dto.Email, dto.Password);

        if (user == null)
        {
            throw new UserInvalidLoginCredentialsException();
        }

        var userDto = Map(user);
        userDto.Token = _userTokenProvider.Create(user);

        return userDto;
    }

    public async Task<UserLoginGetDto> RegisterAsync(UserRegisterPostDto dto)
    {
        var userExists = await _unitOfWork.UserRepository.ExistsAsync(dto.Email);

        if (userExists)
        {
            throw new UserEmailAlreadyExistsException(dto.Email);
        }

        if (dto.Password != dto.ConfirmPassword)
        {
            throw new UserRegistrationPasswordDoNotMatchException();
        }

        var restaurant = dto.IsRestaurant ? new Restaurant() : null;
        var cart = dto.IsRestaurant ? null : new Cart();

        var user = new User
        {
            Email = dto.Email,
            Password = dto.Password,
            CreatedOn = DateTime.Now,
            Restaurant = restaurant,
            Cart = cart
        };

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var dbUser = await _unitOfWork.UserRepository.GetByIdAsync(dto.Email);
        var userDto = Map(dbUser);
        userDto.Token = _userTokenProvider.Create(dbUser);

        return userDto;
    }

    private static UserLoginGetDto Map(User user)
    {
        return new UserLoginGetDto
        {
            Email = user.Email,
            Name = user.Name,
            Phone = user.Phone,
            UserId = user.UserId,
            RestaurantId = user.RestaurantId,
            CartId = user.CartId,
            NumberOfProducts = user.Cart?.Products?.Count ?? 0
        };
    }

    public async Task ChangePasswordAsync(UserChangePasswordDto dto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByCredentials(dto.Email, dto.CurrentPassword);

        if (user == null)
        {
            throw new UserEmailAlreadyExistsException(dto.Email);
        }

        if (dto.NewPassword != dto.ConfirmPassword)
        {
            throw new UserRegistrationPasswordDoNotMatchException();
        }

        user.Password = dto.NewPassword;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<UserAccountDetailsGetDto> GetAccountDetailsAsync()
    {
        var userId = _userSessionProvider.GetUserId();
        var user = await _unitOfWork.UserRepository.GetAsync(userId);

        return new UserAccountDetailsGetDto
        {
            Name = user.Name,
            Phone = user.Phone
        };
    }

    public async Task UpdateAccountDetailsAsync(UserAccountDetailsPostDto dto)
    {
        var userId = _userSessionProvider.GetUserId();
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

        user.Name = dto.Name;
        user.Phone = dto.Phone;

        await _unitOfWork.SaveChangesAsync();
    }
}
