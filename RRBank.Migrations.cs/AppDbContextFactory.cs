using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RRBank.Infra;

namespace RRBank.Migrations.cs
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            // Configuração da connection string a partir de um arquivo appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(connectionString,
                b => b.MigrationsAssembly("RRBank.Infra"));

            return new DataContext(builder.Options);
        }
    }
}