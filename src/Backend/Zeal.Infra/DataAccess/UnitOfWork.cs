using Zeal.Domain.Repositories;

namespace Zeal.Infra.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ZealDbContext _context;

    public UnitOfWork(ZealDbContext context) => _context = context;

    public async Task Commit() => await _context.SaveChangesAsync();
}
