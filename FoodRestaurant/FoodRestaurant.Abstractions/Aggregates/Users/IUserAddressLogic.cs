using FoodRestaurant.Domain.Aggregates.Users;

namespace FoodRestaurant.Abstractions.Aggregates.Users;

public interface IUserAddressLogic
{
    Task<List<UserAddressGetListDto>> GetAllAsync();
    Task AddOrUpdateAsync(UserAddressPostPatchDto dto);
    Task DeleteAsync(int userAddressId);
}
