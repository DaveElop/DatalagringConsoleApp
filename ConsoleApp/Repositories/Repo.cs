using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ConsoleApp.Repositories;
using ConsoleApp.Entities;

namespace ConsoleApp.Repositories
{
    public class Repo : IProductRepository
    {
        private readonly ProductCatalogContext _context;

        public Repo(ProductCatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ProductEntity> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddProductAsync(ProductEntity productEntity)
        {
            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductEntity productEntity)
        {
            _context.Products.Update(productEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var productEntity = await GetProductByIdAsync(id);
            if (productEntity != null)
            {
                _context.Products.Remove(productEntity);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<ProductEntity> GetAllProducts()
        {
            return _context.Products.AsQueryable();
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
        {
            return await Task.FromResult(GetAllProducts().AsEnumerable());
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
