using System;
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
            dbContext.EnsureSchemaAndSeedData();
            this.dbContext = dbContext;
        }

        public async Task<CustomerModel> GetCustomerByLoyaltyNumber(int loyaltyNumber)
        {
            var customers = dbContext.Customers.FromSqlRaw("SELECT * FROM Customers where LoyaltyNumber = " + loyaltyNumber.ToString());
            var customer = await customers.FirstOrDefaultAsync();

            if (customer == null)
                return null;

            return new CustomerModel
            {
                LoyaltyNumber = customer.LoyaltyNumber,
                FavoriteDrink = customer.FavoriteDrink,
                Name = customer.Name,
                Points = customer.Points,
                Id = customer.Id
            };
        }
    }
}
