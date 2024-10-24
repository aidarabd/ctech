using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public abstract class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private const string _connectionStringName = "DbConnection";
        private const string _aspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public TContext CreateDbContext(string[] args)
        {
            string basePath = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}WebAPI";
            return Create(basePath, Environment.GetEnvironmentVariable(_aspNetCoreEnvironment));
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create(string basePath, string environmentName)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(basePath)
                 .AddJsonFile("appsettings.json")
                 .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                 .AddEnvironmentVariables()
                 .Build();

            string connectionString = configuration.GetConnectionString(_connectionStringName);

            return Create(connectionString);
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{_connectionStringName}' is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

            DbContextOptionsBuilder<TContext> optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseNpgsql(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}
