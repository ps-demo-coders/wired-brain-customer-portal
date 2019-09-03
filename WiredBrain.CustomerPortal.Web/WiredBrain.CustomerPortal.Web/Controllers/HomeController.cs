using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WiredBrain.CustomerPortal.Web.Repositories;

namespace WiredBrain.CustomerPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerRepository repo;

        public HomeController(ICustomerRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Enter loyalty number";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int loyaltyNumber)
        {
            var customer = await repo.GetCustomerByLoyaltyNumber(loyaltyNumber);
            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "Unknown loyalty number");
                return View();
            }
            return RedirectToAction("LoyaltyOverview", new { loyaltyNumber });
        }

        public async Task<IActionResult> LoyaltyOverview(int loyaltyNumber)
        {
            ViewBag.Title = "Your points";

            var customer = await repo.GetCustomerByLoyaltyNumber(loyaltyNumber);
            return View(customer);
        }
    }
}
