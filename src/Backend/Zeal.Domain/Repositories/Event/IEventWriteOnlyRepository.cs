namespace Zeal.Domain.Repositories.Event;

public interface IEventWriteOnlyRepository
{
    Task Add(Entities.Event eventEntity);
}
