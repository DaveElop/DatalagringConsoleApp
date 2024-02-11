using System.Threading.Tasks;
using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace ConsoleApp.Repositories
{
    public interface IProductRepository
    {
        Task<ProductEntity> GetProductByIdAsync(int productId);
        Task AddProductAsync(ProductEntity product);
        Task UpdateProductAsync(ProductEntity product);
        Task DeleteProductAsync(int productId);
        Task SaveChangesAsync();
        IQueryable<ProductEntity> GetAllProducts();
        Task<IEnumerable<ProductEntity>> GetAllProductsAsync();

        Task<IDbContextTransaction> BeginTransaction();
    }
}
