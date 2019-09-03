using System.Threading.Tasks;
using WiredBrain.CustomerPortal.Web.Models;

namespace WiredBrain.CustomerPortal.Web.Repositories
{
    public interface ICustomerRepository
    {
        Task<CustomerModel> GetCustomerByLoyaltyNumber(int loyaltyNumber);
    }
}