// IAddressService.cs
using ConsoleApp.Entities;

public interface IAddressService
{
    Task<AddressEntity> GetAddressByIdAsync(int addressId);
    Task<IEnumerable<AddressEntity>> GetAllAddressesAsync();
    Task AddAddressAsync(AddressEntity address);
    Task UpdateAddressAsync(AddressEntity address);
    Task DeleteAddressAsync(int addressId);
}