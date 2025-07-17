using Microsoft.EntityFrameworkCore;
using Zeal.Domain.Repositories.User;

namespace Zeal.Infra.DataAccess.Repositories;

public class UserRepository : IUserWriteOnlyRepository, IUserUpdateOnlyRepository, IUserReadOnlyRepository
{
    private readonly ZealDbContext _context;

    public UserRepository(ZealDbContext context) => _context = context;

    public async Task Add(Domain.Entities.User user) => await _context.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _context.Users.AsNoTracking().AnyAsync(user => user.Email.Equals(email) && user.Active);

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) => await _context.Users.AsNoTracking().AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);

    //public async Task<Domain.Entities.User> GetByUserIdentifier(Guid userIdentifier)
    //{
    //    return await _context
    //        .Users
    //        .AsNoTracking()
    //        .FirstAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);
    //}

    public async Task<Domain.Entities.User?> GetByEmailAndPassword(string email, string password)
    {
        return await _context
            .Users
            .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance
            .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(password) && user.Active);
    }

    public Task<Domain.Entities.User> GetById(long id) => _context.Users.FirstAsync(user => user.Id == id && user.Active);

    public void Update(Domain.Entities.User user) => _context.Users.Update(user);
}