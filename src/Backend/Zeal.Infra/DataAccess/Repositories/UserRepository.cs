using Microsoft.EntityFrameworkCore;
using Zeal.Domain.Repositories.User;

namespace Zeal.Infra.DataAccess.Repositories;

public class UserRepository : IUserWriteOnlyRepository, IUserUpdateOnlyRepository, IUserReadOnlyRepository
{
    private readonly ZealDbContext _context;

    public UserRepository(ZealDbContext context) => _context = context;

    public async Task Add(Domain.Entities.User user) => await _context.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _context.Users.AsNoTracking().AnyAsync(user => user.Email.Equals(email) && user.Active);

    public async Task<Domain.Entities.User?> GetByEmailAndPassword(string email, string password)
    {
        return await _context
            .Users
            .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance
            .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(password) && user.Active);
    }
}