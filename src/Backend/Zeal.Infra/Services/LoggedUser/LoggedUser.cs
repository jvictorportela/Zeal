using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Zeal.Domain.Entities;
using Zeal.Domain.Security.Tokens;
using Zeal.Domain.Services.LoggedUser;
using Zeal.Infra.DataAccess;

namespace Zeal.Infra.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly ZealDbContext _context;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(ZealDbContext context, ITokenProvider tokenProvider)
    {
        _context = context;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        return await _context
            .Users
            .AsNoTracking()
            .FirstAsync(u => u.Active && u.UserIdentifier == Guid.Parse(identifier));
    }
}
