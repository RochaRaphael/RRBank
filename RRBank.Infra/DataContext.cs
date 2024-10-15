using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RRBank.Domain.Database;
using RRBank.Infra;
using RRBank.Infra.Mapping;

namespace RRBank.Infra
{
    public class DataContext : DbContext
    {
        public DataContext() { } // Parameterless constructor, useful for mocking
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<RequestCancellation> RequestCancellation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ManagerMap());
            modelBuilder.ApplyConfiguration(new AccountMap());
        }
    }
}

