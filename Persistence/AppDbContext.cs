using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Domain.Entities;

namespace Persistence;

public class AppDbContext : DbContext, IDbContext
{
    public AppDbContext(){}
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        //here we can change SQL type for our need
        optionsBuilder.UseNpgsql("Server=localhost; Port=5432;Database=c_tech_db; Username=postgres;Password=123; Pooling=true;MinPoolSize=1;MaxPoolSize=200;IncludeErrorDetail=true;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}