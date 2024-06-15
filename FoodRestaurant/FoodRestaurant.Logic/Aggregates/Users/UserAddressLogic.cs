using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Users;

namespace FoodRestaurant.Logic.Aggregates.Users;

public class UserAddressLogic : IUserAddressLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSessionProvider _userSessionProvider;

    public UserAddressLogic(IUnitOfWork unitOfWork, IUserSessionProvider userSessionProvider)
    {
        _unitOfWork = unitOfWork;
        _userSessionProvider = userSessionProvider;
    }

    public async Task AddOrUpdateAsync(UserAddressPostPatchDto dto)
    {
        var userId = _userSessionProvider.GetUserId();

        if (dto.UserAddressId.HasValue)
        {
            var userAddress = await _unitOfWork.UserAddressRepository.GetAsync(dto.UserAddressId.Value);

            userAddress.Address = dto.Address;
            userAddress.City = dto.City;
            userAddress.Street = dto.Street;
        }
        else
        {
            var userAddress = new UserAddress
            {
                Address = dto.Address,
                City = dto.City,
                Street = dto.Street,
                UserId = userId
            };

            await _unitOfWork.UserAddressRepository.AddAsync(userAddress);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int userAddressId)
    {
        var userAddress = await _unitOfWork.UserAddressRepository.GetAsync(userAddressId);

        if (userAddress != null)
        {
            await _unitOfWork.UserAddressRepository.DeleteAsync(userAddress);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<List<UserAddressGetListDto>> GetAllAsync()
    {
        var userId = _userSessionProvider.GetUserId();
        var userAddress = await _unitOfWork.UserAddressRepository.GetAllForUserAsync(userId);

        return userAddress.Select(x => new UserAddressGetListDto
        {
            Address = x.Address,
            City = x.City,
            Street = x.Street,
            UserAddressId = x.UserAddressId
        }).ToList();
    }
}
