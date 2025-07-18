namespace Zeal.Domain.Repositories.Address;

public interface IAddressWriteOnlyRepository
{
    Task Add(Entities.Address address);
}
