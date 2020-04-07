using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WiredBrain.CustomerPortal.Web.Models;
using WiredBrain.CustomerPortal.Web.Repositories;

namespace WiredBrain.CustomerPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerRepository repo;
        private readonly IConfiguration config;
        private const string cookieName = "LoyaltyNumber";

        public HomeController(ICustomerRepository repo, IConfiguration config)
        {
            this.repo = repo;
            this.config = config;
        }

        public IActionResult Index()
        {
            if (Request.Cookies[cookieName] != null)
                return RedirectToAction("LoyaltyOverview");

            ViewBag.Title = "Enter loyalty number";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int loyaltyNumber)
        {
            var customer = await repo.GetCustomerByLoyaltyNumber(loyaltyNumber);
            if (customer == null)
            {
                ModelState.AddModelError(string.Empty,
                    "Unknown loyalty number");
                return View();
            }
            Response.Cookies.Append(cookieName, $"{loyaltyNumber}",
                new CookieOptions { SameSite = SameSiteMode.Lax });
            return RedirectToAction("LoyaltyOverview");
        }

        public async Task<IActionResult> LoyaltyOverview()
        {
            ViewBag.Title = "Your points";

            var customer = await repo.GetCustomerByLoyaltyNumber(
                GetLoyaltyNumberFromCookie());
            var pointsNeeded = int.Parse(config["CustomerPortalSettings:PointsNeeded"]);

            var loyaltyModel = LoyaltyModel.FromCustomer(customer,
                pointsNeeded);

            return View(loyaltyModel);
        }

        public async Task<IActionResult> EditFavorite()
        {
            ViewBag.Title = "Edit favorite";

            var customer = await repo.GetCustomerByLoyaltyNumber(
                GetLoyaltyNumberFromCookie());
            return View(new EditFavoriteModel
            {
                LoyaltyNumber = customer.LoyaltyNumber,
                Favorite = customer.FavoriteDrink
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFavorite(EditFavoriteModel model)
        {
            await repo.SetFavorite(GetLoyaltyNumberFromCookie(),
                model.Favorite);
            return RedirectToAction("LoyaltyOverview");
        }

        public async Task<IActionResult> SetFavorite(string favorite)
        {
            await repo.SetFavorite(GetLoyaltyNumberFromCookie(), favorite);
            return RedirectToAction("LoyaltyOverview");
        }

        private int GetLoyaltyNumberFromCookie()
        {
            var cookie = Request.Cookies[cookieName];
            if (cookie == null)
                throw new ArgumentException("No cookie found");

            return int.Parse(Request.Cookies[cookieName]);
        }
    }
}
