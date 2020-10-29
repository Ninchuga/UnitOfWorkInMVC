using System.Data.Entity;

namespace DAL.Models
{
    public class UnitOfWorkInMVCContext : DbContext, IAmDbContext
    {
        public UnitOfWorkInMVCContext() : base("UnitOfWorkInMVCConnection")
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}