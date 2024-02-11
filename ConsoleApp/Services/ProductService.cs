using ConsoleApp.Entities;
using ConsoleApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductService : IProductService
{
    private readonly ProductCatalogContext _context;

    public ProductService(ProductCatalogContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ProductEntity> GetProductByIdAsync(int productId)
    {
        return await _context.Products.FindAsync(productId);
    }

    public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task AddProductAsync(ProductEntity product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductEntity product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int productId)
    {
        var existingProduct = await _context.Products.FindAsync(productId);
        if (existingProduct != null)
        {
            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> BuyProductAsync(int productId, int quantity, decimal unitPrice)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);

                if (product == null || product.AvailableQuantity < quantity)
                {

                    await transaction.RollbackAsync();
                    return false;
                }


                product.AvailableQuantity -= quantity;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    // Implement other methods...
}
