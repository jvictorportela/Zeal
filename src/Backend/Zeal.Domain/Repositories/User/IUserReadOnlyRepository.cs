﻿namespace Zeal.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<Entities.User?> GetByEmailAndPassword(string email, string password);
    Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
}