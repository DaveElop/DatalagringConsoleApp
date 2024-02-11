using ConsoleApp.Entities;

namespace ConsoleApp.Services;

public interface ICustomerService
{
    Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
    Task<CustomerEntity> GetCustomerByIdAsync(int customerId);
    Task AddCustomerAsync(CustomerEntity customer);
    Task UpdateCustomerAsync(CustomerEntity customer);
    Task DeleteCustomerAsync(int customerId);
}