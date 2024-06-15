using FoodRestaurant.Domain.Aggregates.Reviews;
using FoodRestaurant.Domain.Pages;

namespace FoodRestaurant.Abstractions.Aggregates.Reviews;

public interface IReviewLogic
{
    Task<PageResultDto<ReviewGetListDto>> GetAsync(string searchText, int? currentPage);
    Task<ReviewStatisticsGetDto> GetStatisticsAsync();
}
