using FoodRestaurant.Domain.Aggregates.Categories;
using FoodRestaurant.Domain.Pages;

namespace FoodRestaurant.Abstractions.Aggregates.Categories;

public interface ICategoryLogic
{
    Task PostAsync(CategoryPostDto postDto);
    Task DeleteAsync(List<int> categoryIds);
    Task<PageResultDto<CategoryGetListDto>> GetAsync(string searchText, int? currentPage);
    Task PatchAsync(CategoryPatchDto postDto);
}
