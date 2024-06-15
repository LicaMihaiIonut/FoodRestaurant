using FoodRestaurant.Abstractions.Aggregates.Orders;
using FoodRestaurant.Domain.Aggregates.Orders;
using FoodRestaurant.Domain.Pages;

using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurant.Controllers;

[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderLogic _orderLogic;

    public OrdersController(IOrderLogic orderLogic)
    {
        _orderLogic = orderLogic;
    }

    [HttpGet]
    [Route("dashboard")]
    public async Task<OrderDashboardGetDto> GetDashboard(OrderDashboardType orderDashboardType)
    {
        return await _orderLogic.GetDashboardAsync(orderDashboardType);
    }

    [HttpPost]
    [Route("place")]
    public async Task Place(int addressId, int cardId)
    {
        await _orderLogic.PlaceAsync(addressId, cardId);
    }

    [HttpGet]
    [Route("user")]
    public async Task<List<OrderGetListDto>> GetForUser()
    {
        return await _orderLogic.GetAllForUserAsync();
    }

    [HttpGet]
    [Route("restaurant")]
    public async Task<PageResultDto<OrderGetListDto>> GetForRestaurant(string searchText, int page)
    {
        return await _orderLogic.GetAllForRestaurantAsync(searchText, page);
    }

    [HttpPost]
    [Route("review")]
    public async Task AddReview([FromBody] OrderReviewPostDto postDto)
    {
        await _orderLogic.AddReviewAsync(postDto);
    }

    [HttpPost]
    [Route("status")]
    public async Task ChangeStatus(int orderId, OrderStatus status, DateTime? deliveryTime)
    {
        await _orderLogic.ChangeStatusAsync(orderId, status, deliveryTime);
    }
}
