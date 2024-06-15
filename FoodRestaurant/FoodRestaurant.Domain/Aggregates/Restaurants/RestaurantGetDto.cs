using FoodRestaurant.Domain.Aggregates.Categories;

namespace FoodRestaurant.Domain.Aggregates.Restaurants;

public class RestaurantGetDto
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Delivery { get; set; }
    public decimal? Transport { get; set; }

    public List<CategoryGetListDto> Categories { get; set; }
}
