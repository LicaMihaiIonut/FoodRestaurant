using FoodRestaurant.Abstractions.Aggregates.Categories;
using FoodRestaurant.Domain.Aggregates.Categories;
using FoodRestaurant.Domain.Pages;

using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurant.Controllers;

[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryLogic _categoryLogic;

    public CategoriesController(ICategoryLogic categoryLogic)
    {
        _categoryLogic = categoryLogic;
    }

    [HttpGet]
    public async Task<PageResultDto<CategoryGetListDto>> GetAsync(string searchText, int? currentPage)
    {
        return await _categoryLogic.GetAsync(searchText, currentPage);
    }

    [HttpPost]
    [Route("add")]
    public async Task PostAsync([FromBody] CategoryPostDto postDto)
    {
        await _categoryLogic.PostAsync(postDto);
    }

    [HttpPatch]
    [Route("patch")]
    public async Task PatchAsync([FromBody] CategoryPatchDto postDto)
    {
        await _categoryLogic.PatchAsync(postDto);
    }

    [HttpPost]
    [Route("delete")]
    public async Task DeleteAsync([FromBody] List<int> categoryIds)
    {
        await _categoryLogic.DeleteAsync(categoryIds);
    }
}
