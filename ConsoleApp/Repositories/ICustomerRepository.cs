using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp.Entities;

public interface ICustomerRepository
{
    Task<CustomerEntity> GetCustomerByIdAsync(int customerId);
    Task AddCustomerAsync(CustomerEntity customer);
    Task UpdateCustomerAsync(CustomerEntity customer);
    Task DeleteCustomerAsync(int customerId);
    Task SaveChangesAsync();
    IQueryable<CustomerEntity> GetAllCustomers();
    Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
}
