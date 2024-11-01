using Microsoft.EntityFrameworkCore;

namespace OrderService.Contracts
{
    public interface IOrderDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task SaveChangesAsync();
    }
}
