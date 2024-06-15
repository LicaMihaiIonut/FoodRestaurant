using FoodRestaurant.Domain.BaseEntities;

using System.ComponentModel.DataAnnotations;

namespace FoodRestaurant.Domain.Aggregates.Users;

public class UserCard : TEntity
{
    [Key]
    public int UserCardId { get; set; }
    public int UserId { get; set; }
    public string CardNumber { get; set; }
    public int MonthExpiration { get; set; }
    public int YearExpiration { get; set; }
    public int SecurityCode { get; set; }
    public string Owner { get; set; }

    public User User { get; set; }

}
