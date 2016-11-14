using Restaurant.AppManagers.MainModules;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Restaurant.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Report()
        {
            #region Orders
            ViewData["AllOrders"] = userAppManager.FindAllOrder().Where(a => a.isPay == true).Count();
            ViewData["CountOfFoods"] = userAppManager.FindAllOrderList().Where(a => a.IsPay == true).Count();
            ViewData["OrderBill"] = userAppManager.FindAllOrder().Sum(a => Convert.ToInt32(a.TotalPrice));
            ViewData["CountOfDiscountCodeUsed"] = userAppManager.FindAllOrder().Where(a => a.DiscountCode != null && a.DiscountCode != "").Count();

            ViewData["NewOrderCount"] = userAppManager.FindAllOrder().Where(a => a.OrderDate >= DateTime.Today).Count();
            ViewData["OrderBillToday"] = userAppManager.FindAllOrder().Where(a => a.OrderDate >= DateTime.Today).Sum(a => Convert.ToInt32(a.TotalPrice));
            ViewData["CountOfDiscountCodeUsedToday"] = userAppManager.FindAllOrder().Where(a => a.DiscountCode != null && a.DiscountCode != "" && a.OrderDate >= DateTime.Today).Count();
            #endregion

            #region Users
            ViewData["AllUsers"] = userAppManager.FindAllUser().Count;
            ViewData["CountOfAdmins"] = userAppManager.FindAllUserRole().Where(a => a.RoleId == "1").Count();
            ViewData["CountOfUsers"] = userAppManager.FindAllUserRole().Where(a => a.RoleId == "2").Count();
            #endregion

            #region Courier
            ViewData["CountOfCourier"] = userAppManager.FindAllCourier().Count;
            ViewData["CountOfCourierToday"] = userAppManager.FindAllCourier().Where(a => a.Order.OrderDate >= DateTime.Today).Count();
            #endregion

            #region RestaurantTable
            ViewData["CountOfRestaurantTable"] = userAppManager.FindAllRestaurantTable().Count;
            ViewData["CountOfRestaurantTableToday"] = userAppManager.FindAllRestaurantTable().Where(a => a.Order.OrderDate >= DateTime.Today).Count();
            #endregion

            #region GetInPlcae
            ViewData["CountOfGetInPlace"] = userAppManager.FindAllGetInPlace().Count;
            ViewData["CountOfGetInPlaceToday"] = userAppManager.FindAllGetInPlace().Where(a => a.Order.OrderDate >= DateTime.Today).Count();
            #endregion

            return View();
        }

        public ActionResult Order(string OrderId, string Email,string Date)
        {
            var list = userAppManager.FindAllOrder().Where(a => a.isPay == true).OrderByDescending(a => a.Id);
            ViewBag.OrderStatus = new SelectList(userAppManager.FindAllOrderStatus(),"Id","Status");
            if (OrderId != null && OrderId != "")
            {
                ViewData["OrderId"] = OrderId;
                var listByOrderId = list.Where(a => a.Id == Convert.ToInt32(OrderId));
                return View(listByOrderId);
            }
            else if (Email != null && Email != "")
            {
                ViewData["Email"] = Email;
                var listByEmail = list.Where(a => a.AspNetUser.Email == Email);
                return View(listByEmail);
            }
            else if (Date != null && Date != "")
            {
                ViewData["Date"] = Date;
                var listByDate = list.Where(a => a.Date.DayOfYear == Convert.ToDateTime(Date).DayOfYear);
                return View(listByDate);
            }

            return View(list.Where(a=> a.Date.Day == DateTime.Today.Day));
        }
        
        public ActionResult OrderDetails(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var list = userAppManager.FindAllOrderList().Where(a => a.IsPay == true && a.OrderId == id).OrderByDescending(a => a.OrderDate);
            ViewBag.SumPrice = userAppManager.FindAllOrderList().Where(a => a.IsPay == true && a.OrderId == id).Sum(a => a.Food.Price * a.FoodCount);
            return View(list);

            if (list == null)
            {
                return HttpNotFound();
            }
            return HttpNotFound();
        }

        public ActionResult OrderEdit(string OrderId,string OrderStatus)
        {
            Order order = userAppManager.FindOrder(Convert.ToInt32(OrderId));
            order.OrderStatusId = Convert.ToInt32(OrderStatus);
            userAppManager.EditOrder(order);
            return RedirectToAction("Order");
        }

        public ActionResult OrderPanel()
        {
            return View();
        }

        public ActionResult UserPanel()
        {
            return View();
        }

        public ActionResult FocusPanel()
        {
            return View();
        }


    }
}