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
    public class AOrderListsController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        // GET: AOrderLists
        public ActionResult Index(string Email,string OrderDate,string Date)
        {
            var list = userAppManager.FindAllOrderList().OrderByDescending(a => a.OrderDate);

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
                var listByDate = list.Where(a => a.Order.Date.DayOfYear == Convert.ToDateTime(Date).DayOfYear);
                return View(listByDate);
            }


            return View(list);
        }

        // GET: AOrderLists/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = userAppManager.FindOrderList(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            return View(orderList);
        }
        
        // GET: AOrderLists/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = userAppManager.FindOrderList(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(userAppManager.FindAllUser(), "Id", "Email", orderList.UserId);
            ViewBag.FoodId = new SelectList(userAppManager.FindAllFoods(), "Id", "Name", orderList.FoodId);
            ViewBag.OrderId = new SelectList(userAppManager.FindAllOrder(), "Id", "UserId", orderList.OrderId);
            return View(orderList);
        }

        // POST: AOrderLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FoodId,FoodCount,OrderDate,Note,IsPay,OrderId")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                OrderList orderlist = userAppManager.FindOrderList(orderList.Id);
                orderlist.OrderDate = DateTime.Now;
                orderlist.Note = orderList.Note;
                orderlist.FoodCount = orderList.FoodCount;
                userAppManager.EditOrderList(orderlist);
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(userAppManager.FindAllUser(), "Id", "Email", orderList.UserId);
            ViewBag.FoodId = new SelectList(userAppManager.FindAllFoods(), "Id", "Name", orderList.FoodId);
            ViewBag.OrderId = new SelectList(userAppManager.FindAllOrder(), "Id", "UserId", orderList.OrderId);
            return View(orderList);
        }
        
    }
}
