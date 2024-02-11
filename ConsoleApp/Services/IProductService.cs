// IProductService.cs
using ConsoleApp.Entities;

public interface IProductService
{
    Task<ProductEntity> GetProductByIdAsync(int productId);
    Task<IEnumerable<ProductEntity>> GetAllProductsAsync();
    Task AddProductAsync(ProductEntity product);
    Task UpdateProductAsync(ProductEntity product);
    Task DeleteProductAsync(int productId);
    Task<bool> BuyProductAsync(int productId, int quantity, decimal unitPrice);

}