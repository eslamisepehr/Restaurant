using Microsoft.AspNet.Identity;
using Restaurant.AppManagers.MainModules;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Controllers.User
{
    [Authorize]
    public class TrackController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        // GET: Track
        public ActionResult Index()
        {
            var list = userAppManager.FindAllOrder().Where(a => a.UserId == User.Identity.GetUserId() && a.isPay == true).OrderByDescending(a => a.Id);
            return View(list);
        }

        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var list = userAppManager.FindAllOrderList().Where(a => a.UserId == User.Identity.GetUserId() && a.IsPay == true && a.OrderId == id).OrderByDescending(a => a.OrderDate);
            ViewBag.SumPrice = userAppManager.FindAllOrderList().Where(a => a.UserId == User.Identity.GetUserId() && a.IsPay == true && a.OrderId == id).Sum(a => a.Food.Price * a.FoodCount);
            return View(list);

            if (list == null)
            {
                return HttpNotFound();
            }
            return HttpNotFound();
        }
    }
}