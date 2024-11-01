using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Contracts;
using OrderService.Data;
using OrderService.Models;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("test")]
    public async Task<IActionResult> GetTest()
    {
        return Ok("Hello from OrderService");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderRepository.GetAllOrdersAsync();
        return Ok(orders);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var resultOrder = await _orderRepository.GetOrderByIdAsync(id);
        return resultOrder.IsSuccess ? Ok(resultOrder.Value) : NotFound(resultOrder.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        var resultOrder = await _orderRepository.CreateOrderAsync(order);
        return resultOrder.IsSuccess ? Ok(resultOrder.Value) : BadRequest(resultOrder.Errors);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] Order order)
    {
        var resultOrder = await _orderRepository.UpdateOrderAsync(id, order);
        return resultOrder.IsSuccess ? Ok(resultOrder.Value) : BadRequest(resultOrder.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var resultOrder = await _orderRepository.DeleteOrderAsync(id);
        return resultOrder.IsSuccess ? Ok(resultOrder.Value) : BadRequest(resultOrder.Errors);
    }
}