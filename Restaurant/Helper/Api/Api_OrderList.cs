using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Helper.Api
{
    public class Api_OrderList
    {
        public string FoodName { get; set; }
        public int FoodCount { get; set; }
        public bool isPay { get; set; }
        public string OrderDate { get; set; }
        public int TotalPrice { get; set; }
    }
}