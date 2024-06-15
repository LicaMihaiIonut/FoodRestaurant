using FoodRestaurant.Abstractions.Aggregates.Categories;
using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Categories;
using FoodRestaurant.Domain.Pages;

namespace FoodRestaurant.Logic.Aggregates.Categories;

public class CategoryLogic : ICategoryLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSessionProvider _userSessionProvider;

    public CategoryLogic(IUnitOfWork unitOfWork, IUserSessionProvider userSessionProvider)
    {
        _unitOfWork = unitOfWork;
        _userSessionProvider = userSessionProvider;
    }

    public async Task<PageResultDto<CategoryGetListDto>> GetAsync(string searchText, int? currentPage)
    {
        var restaurantId = _userSessionProvider.GetRestaurantId();

        var categories = await _unitOfWork.CategoryRepository.GetAsync(restaurantId, searchText, currentPage);
        var numberOfRecords = await _unitOfWork.CategoryRepository.GetCountAsync(restaurantId, searchText);

        var entities = categories.Select(x => new CategoryGetListDto
        {
            CategoryId = x.CategoryId,
            Name = x.Name,
            Discount = x.Discount,
            DiscountUntil = x.DiscountUntil,
            IsAvailable = x.IsAvailable,
            NumberOfProducts = x.RestaurantMenus.Count,
        }).ToList();

        return new PageResultDto<CategoryGetListDto>
        {
            Entities = entities,
            NumberOfRecords = numberOfRecords,
        };
    }

    public async Task DeleteAsync(List<int> categoryIds)
    {
        foreach (var categoryId in categoryIds)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(categoryId);

            if (category == null)
            {
                throw new Exception("Category cannot be found.");
            }

            await _unitOfWork.CategoryRepository.DeleteAsync(category);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task PostAsync(CategoryPostDto postDto)
    {
        var category = new Category
        {
            Name = postDto.Name,
            Discount = postDto.Discount,
            DiscountUntil = postDto.DiscountUntil,
            IsAvailable = postDto.IsAvailable,
            RestaurantId = _userSessionProvider.GetRestaurantId()
        };

        await _unitOfWork.CategoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task PatchAsync(CategoryPatchDto postDto)
    {
        var category = await _unitOfWork.CategoryRepository.GetAsync(postDto.CategoryId);

        if (category == null)
        {
            throw new Exception($"Category with id {postDto.CategoryId} was not found");
        }

        category.Name = postDto.Name;
        category.Discount = postDto.Discount;
        category.DiscountUntil = postDto.DiscountUntil;
        category.IsAvailable = postDto.IsAvailable;

        await _unitOfWork.SaveChangesAsync();
    }
}
