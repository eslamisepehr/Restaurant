using Microsoft.AspNet.Identity;
using Restaurant.AppManagers.MainModules;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Controllers.User
{

    public class FoodsController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();
        // GET: Foods
        public ActionResult Index(string Search)
        {
            var list = userAppManager.FindAllFoods();
            if (Search != null && Search != "")
            {
                ViewData["Search"] = Search;
                var listBySearch = list.Where(a => a.Name.Contains(Search));
                return View(listBySearch);
            }
            return View(list);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(int Count, int FoodId, string FoodName, string Note)
        {
            var list = userAppManager.FindAllFoods();

            //[Bind(Include = "Id,UserId,FoodId,FoodCount,OrderDate,Note")] OrderList orderList
            if (ModelState.IsValid)
            {
                ViewBag.statusCount = Count.ToString();
                ViewBag.statusFood = FoodName.ToString();
                DateTime now = DateTime.Now;
                OrderList orderList = new OrderList()
                {
                    UserId = User.Identity.GetUserId(),
                    FoodId = FoodId,
                    FoodCount = Count,
                    OrderDate = now,
                    Note = Note,
                    IsPay = false,
                    OrderId = null
                };

                userAppManager.AddOrderList(orderList);
                return View("Index", list);
            }
            return View(list);
        }
    }
}