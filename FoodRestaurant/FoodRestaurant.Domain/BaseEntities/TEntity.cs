namespace FoodRestaurant.Domain.BaseEntities;

public abstract class TEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
