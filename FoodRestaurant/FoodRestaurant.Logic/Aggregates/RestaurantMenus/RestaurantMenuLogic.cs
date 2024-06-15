using FoodRestaurant.Abstractions.Aggregates.RestaurantMenus;
using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.RestaurantMenus;
using FoodRestaurant.Domain.Pages;

namespace FoodRestaurant.Logic.Aggregates.RestaurantMenus;

public class RestaurantMenuLogic : IRestaurantMenuLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSessionProvider _userSessionProvider;

    public RestaurantMenuLogic(IUnitOfWork unitOfWork, IUserSessionProvider userSessionProvider)
    {
        _unitOfWork = unitOfWork;
        _userSessionProvider = userSessionProvider;
    }

    public async Task DeleteAsync(List<int> restaurantMenuIds)
    {
        foreach (var restaurantMenuId in restaurantMenuIds)
        {
            var restaurantMenu = await _unitOfWork.RestaurantMenuRepository.GetAsync(restaurantMenuId);

            if (restaurantMenu == null)
            {
                throw new Exception("Restaurant product cannot be found.");
            }

            await _unitOfWork.RestaurantMenuRepository.DeleteAsync(restaurantMenu);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PageResultDto<RestaurantMenuGetListDto>> GetAsync(string searchText, int? currentPage)
    {
        var restaurantId = _userSessionProvider.GetRestaurantId();

        var restaurantMenus = await _unitOfWork.RestaurantMenuRepository.GetAsync(restaurantId, searchText, currentPage);
        var numberOfRecords = await _unitOfWork.RestaurantMenuRepository.GetCountAsync(restaurantId, searchText);

        var entities = restaurantMenus.Select(x => new RestaurantMenuGetListDto
        {
            RestaurantMenuId = x.RestaurantMenuId,
            Discount = x.Discount,
            DiscountUntil = x.DiscountUntil,
            Price = x.Price,
            Name = x.Name,
            CategoryName = x.Category.Name,
            CategoryId = x.CategoryId,
            Image = x.Image
        }).ToList();

        return new PageResultDto<RestaurantMenuGetListDto>
        {
            Entities = entities,
            NumberOfRecords = numberOfRecords,
        };
    }

    public async Task PostAsync(RestaurantMenuPostDto postDto)
    {
        if (postDto.RestaurantMenuId.HasValue)
        {
            var restaurantMenu = await _unitOfWork.RestaurantMenuRepository.GetAsync(postDto.RestaurantMenuId.Value);

            restaurantMenu.Price = postDto.Price;
            restaurantMenu.Discount = postDto.Discount;
            restaurantMenu.Name = postDto.Name;
            restaurantMenu.DiscountUntil = postDto.DiscountUntil;
            restaurantMenu.CategoryId = postDto.CategoryId;
        }
        else
        {
            var restaurantMenu = new RestaurantMenu
            {
                Discount = postDto.Discount,
                DiscountUntil = postDto.DiscountUntil,
                Name = postDto.Name,
                Price = postDto.Price,
                CategoryId = postDto.CategoryId,
            };

            await _unitOfWork.RestaurantMenuRepository.AddAsync(restaurantMenu);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UploadImageAsync(RestaurantMenuUploadImagePostDto postDto)
    {
        var restaurantMenu = await _unitOfWork.RestaurantMenuRepository.GetAsync(postDto.MenuRestaurantId);

        restaurantMenu.Image = postDto.Image;

        await _unitOfWork.SaveChangesAsync();
    }
}
