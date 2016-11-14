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
    public class AOrderTypesController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        // GET: AOrderTypes
        public ActionResult Index()
        {
            var list = userAppManager.FindAllOrderType();
            return View(list);
        }

        // GET: AOrderTypes/Details/5
        

        // GET: AOrderTypes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderType orderType = userAppManager.FindOrderType(id);
            if (orderType == null)
            {
                return HttpNotFound();
            }
            return View(orderType);
        }

        // POST: AOrderTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] OrderType orderType)
        {
            if (ModelState.IsValid)
            {
                userAppManager.EditOrderType(orderType);
                return RedirectToAction("Index");
            }
            return View(orderType);
        }
    }
}
