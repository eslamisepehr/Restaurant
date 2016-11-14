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

namespace Restaurant.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AOrdersController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        // GET: AOrders
        public ActionResult Index(string Email, string OrderDate, string Date)
        {
            var list = userAppManager.FindAllOrder().OrderByDescending(a => a.OrderDate);

            if (Email != null && Email != "")
            {
                ViewData["Email"] = Email;
                var listByEmail = list.Where(a => a.AspNetUser.Email.Contains(Email));
                return View(listByEmail);
            }
            else if (OrderDate != null && OrderDate != "")
            {
                ViewData["OrderDate"] = OrderDate;
                var listByOrderDate = list.Where(a => a.OrderDate.DayOfYear == Convert.ToDateTime(OrderDate).DayOfYear);
                return View(listByOrderDate);
            }
            else if (Date != null && Date != "")
            {
                ViewData["Date"] = Date;
                var listByDate = list.Where(a => a.Date.DayOfYear == Convert.ToDateTime(Date).DayOfYear);
                return View(listByDate);
            }

            return View(list);
        }

        // GET: AOrders/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = userAppManager.FindOrder(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: AOrders/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = userAppManager.FindOrder(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(userAppManager.FindAllUser(), "Id", "Email", order.UserId);
            ViewBag.OrderStatusId = new SelectList(userAppManager.FindAllOrderStatus(), "Id", "Status", order.OrderStatusId);
            ViewBag.OrderTypeId = new SelectList(userAppManager.FindAllOrderType(), "Id", "Name", order.OrderTypeId);
            return View(order);
        }

        // POST: AOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderStatusId,OrderTypeId")] Order order)
        {
            if (ModelState.IsValid)
            {
                Order orde = userAppManager.FindOrder(order.Id);
                orde.OrderStatusId = order.OrderStatusId;
                orde.OrderTypeId = order.OrderTypeId;
                userAppManager.EditOrder(orde);
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(userAppManager.FindAllUser(), "Id", "Email", order.UserId);
            ViewBag.OrderStatusId = new SelectList(userAppManager.FindAllOrderStatus(), "Id", "Status", order.OrderStatusId);
            ViewBag.OrderTypeId = new SelectList(userAppManager.FindAllOrderType(), "Id", "Name", order.OrderTypeId);
            return View(order);
        }
        
    }
}
