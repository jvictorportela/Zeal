using Zeal.Domain.Entities;
using Zeal.Domain.Repositories.Address;

namespace Zeal.Infra.DataAccess.Repositories;

public class AddressRepository : IAddressWriteOnlyRepository
{
    private readonly ZealDbContext _dbContext;

    public AddressRepository(ZealDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Address address) => await _dbContext.Addresses.AddAsync(address);
}
