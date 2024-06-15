namespace FoodRestaurant.Domain.Aggregates.Users;

public class UserAddCardPostDto
{
    public int? UserCardId { get; set; }
    public string CardNumber { get; set; }
    public int MonthExpiration { get; set; }
    public int YearExpiration { get; set; }
    public int SecurityCode { get; set; }
    public string Owner { get; set; }
}
