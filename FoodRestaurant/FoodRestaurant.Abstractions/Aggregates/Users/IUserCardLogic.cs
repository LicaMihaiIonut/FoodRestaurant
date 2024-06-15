using FoodRestaurant.Domain.Aggregates.Users;

namespace FoodRestaurant.Abstractions.Aggregates.Users;

public interface IUserCardLogic
{
    Task AddOrUpdateCardAsync(UserAddCardPostDto dto);
    Task<List<UserCardGetListDto>> GetAllAsync();
    Task DeleteAsync(int userCardId);
}
