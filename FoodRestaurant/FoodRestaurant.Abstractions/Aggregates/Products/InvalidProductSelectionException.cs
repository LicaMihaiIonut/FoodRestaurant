namespace FoodRestaurant.Abstractions.Aggregates.Products;

public class InvalidProductSelectionException : Exception
{
    public InvalidProductSelectionException() : base("Invalid product selection.")
    {
    }
}
