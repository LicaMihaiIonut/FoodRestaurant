using FoodRestaurant.Abstractions.Aggregates.Reviews;
using FoodRestaurant.Domain.Aggregates.Reviews;
using FoodRestaurant.Domain.Pages;

using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurant.Controllers;

[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewLogic _reviewLogic;

    public ReviewsController(IReviewLogic reviewLogic)
    {
        _reviewLogic = reviewLogic;
    }

    [HttpGet]
    public async Task<PageResultDto<ReviewGetListDto>> GetAsync(string searchText, int? currentPage)
    {
        return await _reviewLogic.GetAsync(searchText, currentPage);
    }

    [HttpGet("statistics")]
    public async Task<ReviewStatisticsGetDto> GetStatisticsAsync()
    {
        return await _reviewLogic.GetStatisticsAsync();
    }
}
