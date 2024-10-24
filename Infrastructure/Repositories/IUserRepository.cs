using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsername(string email);
    Task<User?> GetById(int id);
    Task UpdateUser(User user);
    Task SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}