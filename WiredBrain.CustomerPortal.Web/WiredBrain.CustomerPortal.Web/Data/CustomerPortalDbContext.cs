using Microsoft.EntityFrameworkCore;

namespace WiredBrain.CustomerPortal.Web.Data
{
    public class CustomerPortalDbContext : DbContext
    {
        public CustomerPortalDbContext(DbContextOptions<CustomerPortalDbContext> options): base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public void EnsureSchemaAndSeedData()
        {
            Database.EnsureCreated();
            Seed();
        }

        private void Seed()
        {
            Customers.Add(new Customer { Name = "Roland", FavoriteDrink = "Latte Macchiato extra strong, no sugar, extra milk", LoyaltyNumber = 5932, Points = 831 });
            Customers.Add(new Customer { Name = "David", FavoriteDrink = "Chai Latte straight up", LoyaltyNumber = 4832, Points = 164 });
            SaveChanges();
        }
    }
}
