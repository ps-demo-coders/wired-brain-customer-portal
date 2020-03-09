﻿using System;
using WiredBrain.CustomerPortal.Web.Data;

namespace WiredBrain.CustomerPortal.Web.Models
{
    public class ProfileModel
    {
        public int LoyaltyNumber { get; set; }
        public string Favorite { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string EmailAddress { get; set; }

        public DateTime BirthDate { get; set; }
        public bool AddLiquor { get; set; }

        public static ProfileModel FromCustomer(Customer customer)
        {
            return new ProfileModel
            {
                LoyaltyNumber = customer.LoyaltyNumber,
                Favorite = customer.FavoriteDrink,
                Name = customer.Name,
                Address = customer.Address,
                Zip = customer.Zip,
                City = customer.City,
                EmailAddress = customer.EmailAddress,
                BirthDate = customer.BirthDate,
                AddLiquor = customer.AddLiquor
            };
        }
    }
}