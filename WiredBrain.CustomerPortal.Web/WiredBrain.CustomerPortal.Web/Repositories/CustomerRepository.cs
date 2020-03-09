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

        public async Task SetProfile(ProfileModel model)
        {
            var customers = dbContext.Customers.FromSqlRaw("SELECT * FROM Customers where LoyaltyNumber = " + model.LoyaltyNumber.ToString());
            var customer = await customers.FirstOrDefaultAsync();

            customer.Name = model.Name;
            customer.Address = model.Address;
            customer.Zip = model.Zip;
            customer.City = model.City;
            customer.AddLiquor = model.AddLiquor;
            customer.BirthDate = model.BirthDate;
            customer.EmailAddress = model.EmailAddress;
            customer.FavoriteDrink = model.Favorite;

            await dbContext.SaveChangesAsync();
        }
    }
}
