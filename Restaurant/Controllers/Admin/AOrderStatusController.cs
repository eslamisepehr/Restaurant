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
    public class AOrderStatusController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        // GET: AOrderStatus
        public ActionResult Index()
        {
            var list = userAppManager.FindAllOrderStatus();
            return View(list);
        }

        // GET: AOrderStatus/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatu orderStatu = userAppManager.FindOrderStatus(id);
            if (orderStatu == null)
            {
                return HttpNotFound();
            }
            return View(orderStatu);
        }

        // GET: AOrderStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AOrderStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status")] OrderStatu orderStatu)
        {
            if (ModelState.IsValid)
            {
                userAppManager.AddOrderStatus(orderStatu);
                return RedirectToAction("Index");
            }

            return View(orderStatu);
        }

        // GET: AOrderStatus/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatu orderStatu = userAppManager.FindOrderStatus(id);
            if (orderStatu == null)
            {
                return HttpNotFound();
            }
            return View(orderStatu);
        }

        // POST: AOrderStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status")] OrderStatu orderStatu)
        {
            if (ModelState.IsValid)
            {
                userAppManager.EditOrderStatus(orderStatu);
                return RedirectToAction("Index");
            }
            return View(orderStatu);
        }

        // GET: AOrderStatus/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatu orderStatu = userAppManager.FindOrderStatus(id);
            if (orderStatu == null)
            {
                return HttpNotFound();
            }
            return View(orderStatu);
        }

        // POST: AOrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderStatu orderStatu = userAppManager.FindOrderStatus(id);
            userAppManager.DeleteOrderStatus(orderStatu);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
