using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Users;

namespace FoodRestaurant.Logic.Aggregates.Users;

public class UserCardLogic : IUserCardLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSessionProvider _userSessionProvider;

    public UserCardLogic(IUnitOfWork unitOfWork, IUserSessionProvider userSessionProvider)
    {
        _unitOfWork = unitOfWork;
        _userSessionProvider = userSessionProvider;
    }

    public async Task AddOrUpdateCardAsync(UserAddCardPostDto dto)
    {
        if (dto.UserCardId.HasValue)
        {
            var userCard = await _unitOfWork.UserCardRepository.GetAsync(dto.UserCardId.Value);

            userCard.CardNumber = dto.CardNumber;
            userCard.YearExpiration = dto.YearExpiration;
            userCard.MonthExpiration = dto.MonthExpiration;
            userCard.CardNumber = dto.CardNumber;
            userCard.Owner = dto.Owner;
            userCard.SecurityCode = dto.SecurityCode;
        }
        else
        {
            var userCard = new UserCard
            {
                SecurityCode = dto.SecurityCode,
                CardNumber = dto.CardNumber,
                MonthExpiration = dto.MonthExpiration,
                Owner = dto.Owner,
                YearExpiration = dto.YearExpiration,
                UserId = _userSessionProvider.GetUserId()
            };

            await _unitOfWork.UserCardRepository.AddAsync(userCard);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int userCardId)
    {
        var userCard = await _unitOfWork.UserCardRepository.GetAsync(userCardId);

        if (userCard != null)
        {
            await _unitOfWork.UserCardRepository.DeleteAsync(userCard);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<List<UserCardGetListDto>> GetAllAsync()
    {
        var userId = _userSessionProvider.GetUserId();
        var userCards = await _unitOfWork.UserCardRepository.GetAllByUserIdAsync(userId);

        return userCards.Select(x => new UserCardGetListDto
        {
            CardNumber = x.CardNumber,
            MonthExpiration = x.MonthExpiration,
            Owner = x.Owner,
            SecurityCode = x.SecurityCode,
            UserCardId = x.UserCardId,
            YearExpiration = x.YearExpiration
        }).ToList();
    }
}
