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
    [Authorize(Roles ="Admin")]
    public class ADiscountCodesController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        // GET: ADiscountCodes
        public ActionResult Index()
        {
            var list = userAppManager.FindAllDiscountCode();
            return View(list);
        }

        // GET: ADiscountCodes/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCode discountCode = userAppManager.FindDiscountCode(id);
            if (discountCode == null)
            {
                return HttpNotFound();
            }
            return View(discountCode);
        }

        // GET: ADiscountCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ADiscountCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Persent")] DiscountCode discountCode)
        {
            if (ModelState.IsValid)
            {
                userAppManager.AddDiscountCode(discountCode);
                return RedirectToAction("Index");
            }

            return View(discountCode);
        }

        // GET: ADiscountCodes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCode discountCode = userAppManager.FindDiscountCode(id);
            if (discountCode == null)
            {
                return HttpNotFound();
            }
            return View(discountCode);
        }

        // POST: ADiscountCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Persent")] DiscountCode discountCode)
        {
            if (ModelState.IsValid)
            {
                userAppManager.EditDiscountCode(discountCode);
                return RedirectToAction("Index");
            }
            return View(discountCode);
        }

        // GET: ADiscountCodes/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCode discountCode = userAppManager.FindDiscountCode(id);
            if (discountCode == null)
            {
                return HttpNotFound();
            }
            return View(discountCode);
        }

        // POST: ADiscountCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiscountCode discountCode = userAppManager.FindDiscountCode(id);
            userAppManager.DeleteDiscountCode(discountCode);
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
