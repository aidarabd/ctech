using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence;

namespace Infrastructure.Repositories;

public class UserRepository(IDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByUsername(string username)
    {
        var user = dbContext.Users.SingleOrDefault(u => u.Username == username);

        return user;
    }

    public async Task<User?> GetById(int id)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateUser(User user)
    {
        dbContext.Users.Update(user);
    }

    public void AddPayment(Payment payment)
    {
        dbContext.Payments.Add(payment);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await dbContext.Database.BeginTransactionAsync();
    }

}