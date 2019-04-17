using EquipmentRental.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EquipmentRental.DataAccess.DbContext
{
    public class SqlDbContext : IdentityDbContext<Customer>
    {
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }
    }
}
