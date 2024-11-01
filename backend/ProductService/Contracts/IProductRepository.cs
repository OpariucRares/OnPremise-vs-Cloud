using ProductService.Models;

namespace ProductService.Contracts;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Result<Product>> GetProductByIdAsync(Guid id);
    Task<Result<Product>> CreateProductAsync(Product product);
    Task<Result<Product>> UpdateProductAsync(Guid id, Product newProduct);
    Task<Result<Product>> DeleteProductAsync(Guid id);
}