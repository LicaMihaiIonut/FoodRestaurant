using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Domain.Aggregates.Users;

using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurant.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly IUserCardLogic _userCardLogic;
    private readonly IUserAddressLogic _userAddressLogic;

    public UsersController(IUserLogic userLogic, IUserCardLogic userCardLogic, IUserAddressLogic userAddressLogic)
    {
        _userLogic = userLogic;
        _userCardLogic = userCardLogic;
        _userAddressLogic = userAddressLogic;
    }

    [HttpPost]
    [Route("register")]
    public async Task<UserLoginGetDto> RegisterAsync(UserRegisterPostDto dto)
    {
        return await _userLogic.RegisterAsync(dto);
    }

    [HttpPost]
    [Route("login")]
    public async Task<UserLoginGetDto> LoginAsync(UserLoginPostDto dto)
    {
        return await _userLogic.LoginAsync(dto);
    }

    [HttpPost]
    [Route("change-password")]
    public async Task ChangePasswordAsync(UserChangePasswordDto dto)
    {
        await _userLogic.ChangePasswordAsync(dto);
    }

    [HttpGet]
    [Route("account-details")]
    public async Task<UserAccountDetailsGetDto> GetAccountDetailsAsync()
    {
        return await _userLogic.GetAccountDetailsAsync();
    }

    [HttpPost]
    [Route("account-details")]
    public async Task UpdateAccountDetailsAsync(UserAccountDetailsPostDto dto)
    {
        await _userLogic.UpdateAccountDetailsAsync(dto);
    }

    [HttpPost]
    [Route("add-update-card")]
    public async Task AddOrUpdateCardAsync(UserAddCardPostDto dto)
    {
        await _userCardLogic.AddOrUpdateCardAsync(dto);
    }

    [HttpGet]
    [Route("cards")]
    public async Task<List<UserCardGetListDto>> GetAllUserCardsAsync()
    {
        return await _userCardLogic.GetAllAsync();
    }

    [HttpPost]
    [Route("card/delete/{userCardId}")]
    public async Task DeleteCardAsync(int userCardId)
    {
        await _userCardLogic.DeleteAsync(userCardId);
    }

    [HttpPost]
    [Route("add-update-address")]
    public async Task AddOrUpdateAddressAsync(UserAddressPostPatchDto dto)
    {
        await _userAddressLogic.AddOrUpdateAsync(dto);
    }

    [HttpGet]
    [Route("addresses")]
    public async Task<List<UserAddressGetListDto>> GetAllUserAddressesAsync()
    {
        return await _userAddressLogic.GetAllAsync();
    }

    [HttpPost]
    [Route("address/delete/{userAddressId}")]
    public async Task DeleteAddressAsync(int userAddressId)
    {
        await _userAddressLogic.DeleteAsync(userAddressId);
    }
}
