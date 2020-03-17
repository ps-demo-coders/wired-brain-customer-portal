﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using WiredBrain.CustomerPortal.Web.Models;
using WiredBrain.CustomerPortal.Web.Repositories;

namespace WiredBrain.CustomerPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerRepository repo;
        private readonly IConfiguration config;

        public HomeController(ICustomerRepository repo, IConfiguration config)
        {
            this.repo = repo;
            this.config = config;
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
            var pointsNeeded = int.Parse(config["CustomerPortalSettings:PointsNeeded"]);

            var loyaltyModel = LoyaltyModel.FromCustomer(customer, pointsNeeded);
            return View(loyaltyModel);
        }

        public async Task<IActionResult> EditProfile(
            [Range(0, 10000), BindRequired, FromQuery] int loyaltyNumber)
        {
            ViewBag.Title = "Edit profile";

            if (ModelState.IsValid)
            {
                var customer = await repo.GetCustomerByLoyaltyNumber(loyaltyNumber);
                return View(ProfileModel.FromCustomer(customer));
            }
            throw new ArgumentException("Invalid loyalty number");
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                await repo.SetProfile(model);
                return RedirectToAction("LoyaltyOverview",
                    new { loyaltyNumber = model.LoyaltyNumber });
            }
            return View(model);

        }
    }
}
