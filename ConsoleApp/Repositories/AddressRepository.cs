using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp.Entities;
using ConsoleApp.Contexts;
using ConsoleApp.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly CustomerCatalogContext _context;

    public AddressRepository(CustomerCatalogContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<AddressEntity> GetAddressByIdAsync(int addressId)
    {
        return await _context.Addresses.FindAsync(addressId);
    }

    public async Task AddAddressAsync(AddressEntity address)
    {
        if (address == null)
        {
            throw new ArgumentNullException(nameof(address));
        }

        await _context.Addresses.AddAsync(address);

    }

    public async Task UpdateAddressAsync(AddressEntity address)
    {
        if (address == null)
        {
            throw new ArgumentNullException(nameof(address));
        }

        _context.Addresses.Update(address);

    }

    public async Task DeleteAddressAsync(int addressId)
    {
        var address = await _context.Addresses.FindAsync(addressId);
        if (address != null)
        {
            _context.Addresses.Remove(address);

        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IQueryable<AddressEntity> GetAllAddresses()
    {
        return _context.Addresses;
    }

    public async Task<IEnumerable<AddressEntity>> GetAllAddressesAsync()
    {
        return await _context.Addresses.ToListAsync();
    }

}
