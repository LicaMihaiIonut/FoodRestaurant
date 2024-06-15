using FoodRestaurant.Domain.Aggregates.Reviews;

namespace FoodRestaurant.Domain.Aggregates.Orders;

public class OrderGetListDto
{
    public int OrderId { get; set; }

    public decimal TotalPrice { get; set; }
    public string Address { get; set; }
    public string Card { get; set; }
    public string RestaurantName { get; set; }
    public int RestaurantId { get; set; }
    public DateTime? CreatedOn { get; set; }
    public OrderStatus? Status { get; set; }
    public string UserName { get; set; }
    public string Phone { get; set; }
    public DateTime? DeliveryTime { get; set; }

    public ReviewGetListDto Review { get; set; }
    public List<OrderProductGetListDto> Products { get; set; }
}
