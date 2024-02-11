using ConsoleApp.Contexts;
using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConsoleApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerCatalogContext _context;

        public CustomerService(ICustomerRepository customerRepository, CustomerCatalogContext context)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _context = context ?? throw new ArgumentNullException(nameof(DbContext));
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<CustomerEntity> GetCustomerByIdAsync(int customerId)
        {
            return await _customerRepository.GetCustomerByIdAsync(customerId);
        }

        public async Task AddCustomerAsync(CustomerEntity customer)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _customerRepository.AddCustomerAsync(customer);


                    await _customerRepository.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    throw;
                }
            }
        }


        public async Task UpdateCustomerAsync(CustomerEntity customer)
        {
            await _customerRepository.UpdateCustomerAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            await _customerRepository.DeleteCustomerAsync(customerId);
            await _customerRepository.SaveChangesAsync();
        }
    }
}