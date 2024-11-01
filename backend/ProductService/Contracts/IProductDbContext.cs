using Microsoft.EntityFrameworkCore;

namespace ProductService.Contracts
{
    public interface IProductDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task SaveChangesAsync();
    }
}