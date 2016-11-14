using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Helper.Api
{
    public class CApi
    {
        public class Info
        {
            public string Count { get; set; }
            public string About { get; set; }
            public string PictureLink { get; set; }
            public string ContactUs { get; set; }
            public string PhoneNumber { get; set; }
        }

        public class Track
        {
            public int Id { get; set; }
            public string OrderDate { get; set; }
            public string Date { get; set; }
            public int TotalPrice { get; set; }
            public bool isPay { get; set; }
            public string OrderTypeName { get; set; }
            public string OrderStatuStatus { get; set; }
            public string DiscountCode { get; set; }
            public int FoodCount { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public int TableCount { get; set; }
        }

        public class OrderList
        {
            public string FoodName { get; set; }
            public int FoodCount { get; set; }
            public bool isPay { get; set; }
            public string OrderDate { get; set; }
            public int TotalPrice { get; set; }
        }

        public class RegisterValue
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}