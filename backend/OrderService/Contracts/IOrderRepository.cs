using OrderService.Data;
using OrderService.Models;

namespace OrderService.Contracts;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Result<Order>> GetOrderByIdAsync(Guid id);
    Task<Result<Order>> CreateOrderAsync(Order order);
    Task<Result<Order>> UpdateOrderAsync(Guid id, Order newOrder);
    Task<Result<Order>> DeleteOrderAsync(Guid id);
}