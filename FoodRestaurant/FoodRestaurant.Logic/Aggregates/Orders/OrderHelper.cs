namespace FoodRestaurant.Logic.Aggregates.Orders;

public static class OrderHelper
{
    public static decimal CalculatePercentage(decimal value1, decimal value2)
    {
        if (value1 == value2 || value2 == 0)
        {
            return 0;
        }

        if (value1 > value2)
        {
            return (value1 * 100) / value2;
        }

        return -(100 - (value1 * 100) / value2);
    }
}
