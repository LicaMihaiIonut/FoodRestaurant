namespace FoodRestaurant.Domain.Aggregates.Restaurants;

public class RestaurantProfileGetDto
{
    public string Image { get; set; }
    public string Name { get; set; }
    public string Delivery { get; set; }
    public decimal? Transport { get; set; }
    public bool? IsValidated { get; set; }
}
