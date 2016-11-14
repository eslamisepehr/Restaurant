using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models.Entities;
using Restaurant.AppManagers.MainModules;
using Microsoft.AspNet.Identity;

namespace Restaurant.Controllers.Admin
{
    [Authorize(Roles ="Admin")]
    public class ACouriersController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        public ActionResult Index(string OrderId, string Email, string OrderDate, string Date)
        {
            var list = userAppManager.FindAllCourier().OrderByDescending(a=>a.Id);

            if (OrderId != null && OrderId != "")
            {
                ViewData["OrderId"] = OrderId;
                var listByOrderId = list.Where(a => a.OrderId == Convert.ToInt32(OrderId));
                return View(listByOrderId);
            }
            else if (Email != null && Email != "")
            {
                ViewData["Email"] = Email;
                var listByEmail = list.Where(a => a.AspNetUser.Email.Contains(Email));
                return View(listByEmail);
            }
            else if (OrderDate != null && OrderDate != "")
            {
                ViewData["OrderDate"] = OrderDate;
                var listByOrderDate = list.Where(a => a.Order.OrderDate.DayOfYear == Convert.ToDateTime(OrderDate).DayOfYear);
                return View(listByOrderDate);
            }
            else if (Date != null && Date != "")
            {
                ViewData["Date"] = Date;
                var listByDate = list.Where(a => a.Order.Date.DayOfYear == Convert.ToDateTime(Date).DayOfYear);
                return View(listByDate);
            }

            return View(list);
        }
        
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courier courier = userAppManager.FindCourier(id);
            if (courier == null)
            {
                return HttpNotFound();
            }
            return View(courier);
        }
    }
}
