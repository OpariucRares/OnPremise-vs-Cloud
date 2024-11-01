using Microsoft.EntityFrameworkCore;
using OrderService.Contracts;
using OrderService.Models;

namespace OrderService.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderDbContext _databaseOrderDbContext;

        public OrderRepository(IOrderDbContext databaseOrderDbContext)
        {
            _databaseOrderDbContext = databaseOrderDbContext;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _databaseOrderDbContext.Set<Order>().ToListAsync();
        }

        public async Task<Result<Order>> GetOrderByIdAsync(Guid id)
        {
            var entity = await _databaseOrderDbContext.Set<Order>().FindAsync(id);
            return entity == null ? Result<Order>.Error($"{nameof(Order)} not found") : Result<Order>.Ok(entity);
        }

        public async Task<Result<Order>> CreateOrderAsync(Order order)
        {
            try
            {
                await _databaseOrderDbContext.Set<Order>().AddAsync(order);

                await _databaseOrderDbContext.SaveChangesAsync();

                return Result<Order>.Ok(order);
            }
            catch (Exception ex)
            {
                return Result<Order>.Error(ex.Message);
            }
        }

        public async Task<Result<Order>> UpdateOrderAsync(Guid id, Order newOrder)
        {
            try
            {
                var order = await _databaseOrderDbContext.Set<Order>().FindAsync(id);
                if (order == null)
                {
                    return Result<Order>.Error($"{nameof(Order)} not found");
                }

                _databaseOrderDbContext.Set<Order>().Update(order);
                await _databaseOrderDbContext.SaveChangesAsync();

                return Result<Order>.OkOnlyMessage("Successful Update");
            }
            catch (Exception ex)
            {
                return Result<Order>.Error(ex.Message);
            }
        }

        public async Task<Result<Order>> DeleteOrderAsync(Guid id)
        {
            try
            {
                var order = await _databaseOrderDbContext.Set<Order>().FindAsync(id);

                if (order == null)
                {
                    return Result<Order>.Error($"{nameof(Order)} not found");
                }

                _databaseOrderDbContext.Set<Order>().Remove(order);

                await _databaseOrderDbContext.SaveChangesAsync();

                return Result<Order>.OkOnlyMessage("Successful Delete");
            }
            catch (Exception ex)
            {
                return Result<Order>.Error(ex.Message);
            }
        }
    }
}
