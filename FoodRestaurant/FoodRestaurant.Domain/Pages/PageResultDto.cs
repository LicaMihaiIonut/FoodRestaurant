namespace FoodRestaurant.Domain.Pages;

public class PageResultDto<T> where T : class
{
    public int NumberOfRecords { get; set; }
    public IEnumerable<T> Entities { get; set; }
}
