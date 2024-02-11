using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp.Entities;

public interface IAddressRepository
{
    Task<AddressEntity> GetAddressByIdAsync(int addressId);
    Task AddAddressAsync(AddressEntity address);
    Task UpdateAddressAsync(AddressEntity address);
    Task DeleteAddressAsync(int addressId);
    Task SaveChangesAsync();
    IQueryable<AddressEntity> GetAllAddresses();
    Task<IEnumerable<AddressEntity>> GetAllAddressesAsync();
}
