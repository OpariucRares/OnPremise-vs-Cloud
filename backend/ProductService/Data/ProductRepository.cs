using Microsoft.EntityFrameworkCore;
using ProductService.Contracts;
using ProductService.Models;

namespace ProductService.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductDbContext _databaseProductDbContext;

        public ProductRepository(IProductDbContext databaseProductDbContext)
        {
            _databaseProductDbContext = databaseProductDbContext;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _databaseProductDbContext.Set<Product>().ToListAsync();
        }

        public async Task<Result<Product>> GetProductByIdAsync(Guid id)
        {
            try
            {
                var entity = await _databaseProductDbContext.Set<Product>().FindAsync(id);
                return entity == null ? Result<Product>.Error($"{nameof(Product)} not found") : Result<Product>.Ok(entity);
            }
            catch (Exception ex)
            {
                return Result<Product>.Error(ex.Message);
            }
        }

        public async Task<Result<Product>> CreateProductAsync(Product product)
        {
            try
            {
                await _databaseProductDbContext.Set<Product>().AddAsync(product);

                await _databaseProductDbContext.SaveChangesAsync();

                return Result<Product>.Ok(product);
            }
            catch (Exception ex)
            {
                return Result<Product>.Error(ex.Message);
            }
        }

        public async Task<Result<Product>> UpdateProductAsync(Guid id, Product newProduct)
        {
            try
            {
                var product = await _databaseProductDbContext.Set<Product>().FindAsync(id);
                if (product == null)
                {
                    return Result<Product>.Error($"{nameof(Product)} not found");
                }

                _databaseProductDbContext.Set<Product>().Update(newProduct);
                await _databaseProductDbContext.SaveChangesAsync();

                return Result<Product>.OkOnlyMessage("Successful Update");
            }
            catch (Exception ex)
            {
                return Result<Product>.Error(ex.Message);
            }
        }

        public async Task<Result<Product>> DeleteProductAsync(Guid id)
        {
            try
            {
                var product = await _databaseProductDbContext.Set<Product>().FindAsync(id);

                if (product == null)
                {
                    return Result<Product>.Error($"{nameof(Product)} not found");
                }

                _databaseProductDbContext.Set<Product>().Remove(product);

                await _databaseProductDbContext.SaveChangesAsync();

                return Result<Product>.OkOnlyMessage("Successful Delete");
            }
            catch (Exception ex)
            {
                return Result<Product>.Error(ex.Message);
            }
        }
    }
}
