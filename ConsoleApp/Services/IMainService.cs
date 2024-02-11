using ConsoleApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMainService
{
    // Customer
    Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
    Task<CustomerEntity> GetCustomerByIdAsync(int customerId);
    Task AddCustomerAsync(CustomerEntity customer);
    Task UpdateCustomerAsync(CustomerEntity customer);
    Task DeleteCustomerAsync(int customerId);

    // Product
    Task<IEnumerable<ProductEntity>> GetAllProductsAsync();
    Task<ProductEntity> GetProductByIdAsync(int productId);
    Task AddProductAsync(ProductEntity product);
    Task UpdateProductAsync(ProductEntity product);
    Task DeleteProductAsync(int productId);
}
