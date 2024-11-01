using Microsoft.AspNetCore.Mvc;
using ProductService.Contracts;
using ProductService.Models;


[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return Ok(products);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var resultProduct = await _productRepository.GetProductByIdAsync(id);
        return resultProduct.IsSuccess ? Ok(resultProduct.Value) : NotFound(resultProduct.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        var resultProduct = await _productRepository.CreateProductAsync(product);
        return resultProduct.IsSuccess ? Ok(resultProduct.Value) : BadRequest(resultProduct.Errors);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
    {
        var resultProduct = await _productRepository.UpdateProductAsync(id, product);
        return resultProduct.IsSuccess ? Ok(resultProduct.Value) : BadRequest(resultProduct.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var resultProduct = await _productRepository.DeleteProductAsync(id);
        return resultProduct.IsSuccess ? Ok(resultProduct.Value) : BadRequest(resultProduct.Errors);
    }
}