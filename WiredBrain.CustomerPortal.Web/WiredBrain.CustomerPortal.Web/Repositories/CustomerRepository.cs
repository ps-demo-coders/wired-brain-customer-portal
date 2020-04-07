using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WiredBrain.CustomerPortal.Web.Data;
using WiredBrain.CustomerPortal.Web.Models;

namespace WiredBrain.CustomerPortal.Web.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerPortalDbContext dbContext;

        public CustomerRepository(CustomerPortalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Customer> GetCustomerByLoyaltyNumber(int loyaltyNumber)
        {
            var customers = dbContext.Customers.FromSqlRaw("SELECT * FROM Customers where LoyaltyNumber = " + loyaltyNumber.ToString());
            var customer = await customers.FirstOrDefaultAsync();

            return customer;
        }

        public async Task SetFavorite(int loyaltyNumber, string favorite)
        {
            var customers = dbContext.Customers.FromSqlRaw("SELECT * FROM Customers where LoyaltyNumber = " + loyaltyNumber.ToString());
            var customer = await customers.FirstOrDefaultAsync();

            customer.FavoriteDrink = favorite;
            await dbContext.SaveChangesAsync();
        }
    }
}
