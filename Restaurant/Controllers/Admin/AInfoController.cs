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
    public class AInfoController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        // GET: AInfo
        public ActionResult Index()
        {
            var list = userAppManager.FindAllInfo();
            return View(list);
        }

        // GET: AInfo/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = userAppManager.FindInfo(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        // GET: AInfo/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = userAppManager.FindInfo(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        // POST: AInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PictureLink,Count,About,ContactUs,ReserveCourierPrice,ReserveTablePrice")] Info info)
        {
            if (ModelState.IsValid)
            {
                userAppManager.EditInfo(info);
                return RedirectToAction("Index");
            }
            return View(info);
        }

        // GET: AInfo/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = userAppManager.FindInfo(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        // POST: AInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Info info = userAppManager.FindInfo(id);
            userAppManager.DeleteInfo(info);
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
