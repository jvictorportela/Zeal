namespace Zeal.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<Entities.User> User();
}
