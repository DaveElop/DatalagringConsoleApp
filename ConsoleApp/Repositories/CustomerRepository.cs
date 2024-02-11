using ConsoleApp.Contexts;
using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerCatalogContext _context;

    public CustomerRepository(CustomerCatalogContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CustomerEntity> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Customers.FindAsync(customerId);
    }

    public async Task AddCustomerAsync(CustomerEntity customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _context.Customers.AddAsync(customer);


                await SaveChangesAsync();
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
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                _context.Customers.Update(customer);

                await SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    public async Task DeleteCustomerAsync(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer != null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Customers.Remove(customer);
                    await SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IQueryable<CustomerEntity> GetAllCustomers()
    {
        return _context.Customers;
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

}
