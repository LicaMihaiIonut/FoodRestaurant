using FoodRestaurant.Abstractions.Aggregates.Restaurants;
using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Abstractions.Context;
using FoodRestaurant.Domain.Aggregates.Restaurants;

namespace FoodRestaurant.Logic.Aggregates.Restaurants;

public class RestaurantLogic : IRestaurantLogic
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSessionProvider _userSessionProvider;

    public RestaurantLogic(IUnitOfWork unitOfWork, IUserSessionProvider userSessionProvider)
    {
        _unitOfWork = unitOfWork;
        _userSessionProvider = userSessionProvider;
    }

    public async Task<List<ResturantGetListDto>> GetAsync()
    {
        var restaurants = await _unitOfWork.RestaurantRepository.GetRestaurantsAsync();

        return restaurants.OrderByDescending(x => x.Schedule != null).ToList();
    }

    public async Task<RestaurantGetDto> GetAsync(int restaurantId)
    {
        return await _unitOfWork.RestaurantRepository.GetRestaurantAsync(restaurantId);
    }

    public async Task<RestaurantProfileGetDto> GetProfileAsync()
    {
        var restaurantId = _userSessionProvider.GetRestaurantId();
        var restaurant = await _unitOfWork.RestaurantRepository.GetAsync(restaurantId);

        return new RestaurantProfileGetDto
        {
            Delivery = restaurant.Delivery,
            Image = restaurant.Image,
            Name = restaurant.Name,
            Transport = restaurant.Transport,
            IsValidated = restaurant.IsValidated
        };
    }

    public async Task UpdateProfileAsync(RestaurantProfilePostDto postDto)
    {
        var restaurantId = _userSessionProvider.GetRestaurantId();
        var restaurant = await _unitOfWork.RestaurantRepository.GetAsync(restaurantId);

        restaurant.Delivery = postDto.Delivery;
        restaurant.Image = postDto.Image;
        restaurant.Name = postDto.Name;
        restaurant.Transport = postDto.Transport;

        restaurant.IsValidated = !(string.IsNullOrWhiteSpace(restaurant.Name) ||
            string.IsNullOrWhiteSpace(restaurant.Image) ||
            !restaurant.Transport.HasValue ||
            string.IsNullOrWhiteSpace(restaurant.Delivery));

        await _unitOfWork.SaveChangesAsync();
    }
}
