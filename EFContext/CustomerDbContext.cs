using CustomerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFContext
{
    public class CustomerDbContext : DbContext
    // REf: https://learn.microsoft.com/en-gb/ef/core/
    {
        public DbSet<CustomerAPI.Entities.Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var localComputerName = "DESKTOP-NQCJC2O";
            optionsBuilder.UseSqlServer($"Server={localComputerName};Database=customers;Integrated Security=true;ConnectRetryCount=0");
            // BTW you need to initially set up a certificate for SQL Server, thus:
            // https://manage.accuwebhosting.com/knowledgebase/2924/Fix--Server-Unable-to-load-user-specified-certificate.-The-server-will-not-accept-a-connection.html
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomerAPI.Entities.Customer>(entity =>
            { 
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }

    // About using code-first
    // Ref: https://docs.oracle.com/cd/E17952_01/connector-net-en/connector-net-entityframework-core-example.html
}
