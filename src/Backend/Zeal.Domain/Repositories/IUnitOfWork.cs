namespace Zeal.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}
