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
    public class AFoodsController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        // GET: AFoods
        public ActionResult Index(string Name)
        {
            var list = userAppManager.FindAllFoods();

            if (Name != null && Name != "")
            {
                ViewData["Name"] = Name;
                var listByName = list.Where(a => a.Name.Contains(Name));
                return View(listByName);
            }
            return View(list);
        }

        // GET: AFoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = userAppManager.FindFood(id.Value);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: AFoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AFoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Price,Describe,Picture")] Food food)
        {
            if (ModelState.IsValid)
            {
                userAppManager.AddFood(food);
                return RedirectToAction("Index");
            }

            return View(food);
        }

        // GET: AFoods/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = userAppManager.FindFood(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: AFoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Describe,Picture")] Food food)
        {
            if (ModelState.IsValid)
            {
                userAppManager.EditFood(food);
                return RedirectToAction("Index");
            }
            return View(food);
        }

        // GET: AFoods/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = userAppManager.FindFood(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: AFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = userAppManager.FindFood(id);
            userAppManager.DeleteFood(food);
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
