using Zeal.Domain.Entities;
using Zeal.Domain.Repositories.Event;

namespace Zeal.Infra.DataAccess.Repositories;

public class EventRepository : IEventWriteOnlyRepository
{
    private readonly ZealDbContext _dbContext;

    public EventRepository(ZealDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Event eventEntity) => await _dbContext.Events.AddAsync(eventEntity);
}
