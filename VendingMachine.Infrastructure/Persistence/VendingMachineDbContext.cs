using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Entities;

namespace VendingMachine.Infrastructure.Persistence
{
    public class VendingMachineDbContext : DbContext
    {
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "VendingMachineDb");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
