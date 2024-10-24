using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Persistence;

public interface IDbContext : IDisposable
{
    public DbSet<User> Users { get; set; }
    public DbSet<Payment> Payments { get; set; }
    //public void Dispose();
    Task<int> SaveChangesAsync();
    DatabaseFacade Database { get; }
}