using ConsoleApp.Entities;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
    }

    public async Task<AddressEntity> GetAddressByIdAsync(int addressId)
    {
        return await _addressRepository.GetAddressByIdAsync(addressId);
    }

    public async Task<IEnumerable<AddressEntity>> GetAllAddressesAsync()
    {
        return await _addressRepository.GetAllAddressesAsync();
    }

    public async Task AddAddressAsync(AddressEntity address)
    {
        if (address == null)
        {
            throw new ArgumentNullException(nameof(address));
        }

        await _addressRepository.AddAddressAsync(address);
        await _addressRepository.SaveChangesAsync();
    }

    public async Task UpdateAddressAsync(AddressEntity address)
    {
        if (address == null)
        {
            throw new ArgumentNullException(nameof(address));
        }

        await _addressRepository.UpdateAddressAsync(address);
        await _addressRepository.SaveChangesAsync();
    }

    public async Task DeleteAddressAsync(int addressId)
    {
        await _addressRepository.DeleteAddressAsync(addressId);
        await _addressRepository.SaveChangesAsync();
    }
}