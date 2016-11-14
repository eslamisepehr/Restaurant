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
using Restaurant.Helper;

namespace Restaurant.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AUserRolesController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();
        RoleName role = new RoleName();

        // GET: AUserRoles
        public ActionResult Index(string Email,string Admin)
        {
            var list = userAppManager.FindAllUserRole();
            
            foreach (var item in list)
            {
                item.RoleId = role.Get(item.RoleId, null);
            }

            if (Email != null && Email != "")
            {
                ViewData["Email"] = Email;
                var listByEmail = list.Where(a => a.AspNetUser.Email.Contains(Email));
                return View(listByEmail);
            }
            else if (Admin != null && Admin != "")
            {
                var listByAdmin = list.Where(a => a.RoleId == "Admin");
                return View(listByAdmin);
            }

            return View(list);
        }        

        // GET: AUserRoles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRole aspNetUserRole = userAppManager.FindUserRole(id);
            aspNetUserRole.RoleId = role.Get(aspNetUserRole.RoleId, null);
            if (aspNetUserRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(role.List());
            return View(aspNetUserRole);
        }

        // POST: AUserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,RoleId")] AspNetUserRole aspNetUserRole)
        {
            if (ModelState.IsValid)
            {
                aspNetUserRole.RoleId = role.Get(null, aspNetUserRole.RoleId);
                userAppManager.EditUserRole(aspNetUserRole);
                //AspNetUserRole aspnetUserRole = userAppManager.FindUserRole(aspNetUserRole.UserId);
                //aspnetUserRole.RoleId = aspNetUserRole.RoleId;
                

                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }
        
    }
}
