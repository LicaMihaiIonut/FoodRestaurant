using FoodRestaurant.Abstractions.Aggregates.Restaurants;
using FoodRestaurant.Domain.Aggregates.Categories;
using FoodRestaurant.Domain.Aggregates.RestaurantMenus;
using FoodRestaurant.Domain.Aggregates.Restaurants;
using FoodRestaurant.Domain.Aggregates.Schedules;
using FoodRestaurant.Domain.Context;

using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Repositories.Aggregates.Restaurants;

public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(FoodRestaurantContext dbContext) : base(dbContext)
    {
    }

    public Task<List<ResturantGetListDto>> GetRestaurantsAsync()
    {
        return _dbContext.Restaurants.Where(x => x.IsValidated == true)
            .Select(x => new ResturantGetListDto
            {
                RestaurantId = x.RestaurantId,
                Name = x.Name,
                Delivery = x.Delivery,
                Transport = x.Transport,
                Image = x.Image,
                AverageGrade = x.AverageGrade,
                TotalReviewNumber = x.TotalReviewNumber,
                Schedule = x.Schedule.Where(x => x.Start.HasValue)
                .Where(x => x.Start.Value.Year == DateTime.Today.Year)
                .Where(x => x.Start.Value.Month == DateTime.Today.Month)
                .Where(x => x.Start.Value.Day == DateTime.Today.Day)
                .Select(x => new ScheduleGetListDto
                {
                    Start = x.Start,
                    End = x.End,
                    IsFreeDay = x.IsFreeDay
                })
                .FirstOrDefault()
            })
            .ToListAsync();
    }

    public Task<RestaurantGetDto> GetRestaurantAsync(int id)
    {
        return _dbContext.Restaurants.Where(x => x.RestaurantId == id)
            .Select(x => new RestaurantGetDto
            {
                Delivery = x.Delivery,
                Image = x.Image,
                Name = x.Name,
                RestaurantId = x.RestaurantId,
                Transport = x.Transport,
                Categories = x.Categories.Where(x => x.IsAvailable).Select(y => new CategoryGetListDto
                {
                    CategoryId = y.CategoryId,
                    Discount = y.Discount,
                    DiscountUntil = y.DiscountUntil,
                    Name = y.Name,
                    Menu = y.RestaurantMenus.Select(z => new RestaurantMenuGetListDto
                    {
                        Discount = z.Discount,
                        DiscountUntil = z.DiscountUntil,
                        Image = z.Image,
                        Name = z.Name,
                        Price = z.Price,
                        RestaurantMenuId = z.RestaurantMenuId
                    }).ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public Task<int> GetRestaurantByProductAsync(int productId)
    {
        return _dbContext.RestaurantMenus.Where(x => x.RestaurantMenuId == productId)
            .Select(x => x.Category.RestaurantId)
            .FirstOrDefaultAsync();
    }

    public Task<Restaurant> GetByOrderId(int orderId)
    {
        return _dbContext.Orders.Include(x => x.Restaurant).Where(x => x.OrderId == orderId).Select(x => x.Restaurant).FirstOrDefaultAsync();
    }
}
