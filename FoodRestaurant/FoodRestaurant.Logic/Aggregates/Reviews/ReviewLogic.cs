using FoodRestaurant.Abstractions.Aggregates.Reviews;
using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Reviews;
using FoodRestaurant.Domain.Pages;

namespace FoodRestaurant.Logic.Aggregates.Reviews;

public class ReviewLogic : IReviewLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSessionProvider _userSessionProvider;

    public ReviewLogic(IUnitOfWork unitOfWork, IUserSessionProvider userSessionProvider)
    {
        _unitOfWork = unitOfWork;
        _userSessionProvider = userSessionProvider;
    }

    public async Task<PageResultDto<ReviewGetListDto>> GetAsync(string searchText, int? currentPage)
    {
        var restaurantId = _userSessionProvider.GetRestaurantId();

        var reviews = await _unitOfWork.ReviewRepository.GetAsync(restaurantId, searchText, currentPage);
        var numberOfRecords = await _unitOfWork.ReviewRepository.GetCountAsync(restaurantId, searchText);

        var entities = reviews.Select(x => new ReviewGetListDto
        {
            UserName = x.Order.User.Name,
            Description = x.Description,
            Grade = x.Grade,
            PostedOn = x.PostedOn
        }).ToList();

        return new PageResultDto<ReviewGetListDto>
        {
            Entities = entities,
            NumberOfRecords = numberOfRecords,
        };
    }

    public async Task<ReviewStatisticsGetDto> GetStatisticsAsync()
    {
        var restaurantId = _userSessionProvider.GetRestaurantId();

        var reviews = await _unitOfWork.ReviewRepository.GetAllGradesAsync(restaurantId);
        var reviewCount = reviews.Count > 0 ? reviews.Count : 1;

        decimal averageGrade = (decimal)reviews.Sum() / reviewCount;

        return new ReviewStatisticsGetDto
        {
            AverageGrade = Math.Round(averageGrade, 2),
            TotalReviewNumber = reviews.Count
        };
    }
}
