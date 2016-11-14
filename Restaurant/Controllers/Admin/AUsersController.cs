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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Restaurant.Models;
using Restaurant.Helper;

namespace Restaurant.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AUsersController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();
        RoleName role = new RoleName();

        // GET: AUsers
        public ActionResult Index(string Email, string PhoneNumber, string Admin)
        {
            var list = userAppManager.FindAllUser();

            foreach (var item in list)
            {
                item.AspNetUserRole.RoleId = role.Get(item.AspNetUserRole.RoleId, null);
            }

            if (Email != null && Email != "")
            {
                ViewData["Email"] = Email;
                var listByEmail = list.Where(a => a.Email.Contains(Email));
                return View(listByEmail);
            }
            else if (PhoneNumber != null && PhoneNumber != "")
            {
                ViewData["PhoneNumber"] = PhoneNumber;
                var listByPhoneNumber = list.Where(a => a.PhoneNumber == PhoneNumber);
                return View(listByPhoneNumber);
            }
            else if (Admin != null && Admin != "")
            {
                var listByAdmin = list.Where(a => a.AspNetUserRole.RoleId == "Admin");
                return View(listByAdmin);
            }

            return View(list);
        }

        // GET: AUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = userAppManager.FindUser(id);
            aspNetUser.AspNetUserRole.RoleId = role.Get(aspNetUser.AspNetUserRole.RoleId, null);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: AUsers/Create
        public ActionResult Create()
        {
            //ViewBag.Id = new SelectList(userAppManager.FindAllUserRole(), "UserId", "RoleId");
            ViewBag.RoleId = new SelectList(role.List());
            return View();
        }

        // POST: AUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "confirmPasword,Password,Email")] RegisterViewModel model, [Bind(Include = "RoleId")] string RoleId, [Bind(Include = "PhoneNumber")] string PhoneNumber)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email , PhoneNumber = PhoneNumber};
                GetManager().Create(user, model.Password);
                GetManager().AddToRoles(user.Id, RoleId);
                
                return RedirectToAction("Index", "AUsers");
            }

            return RedirectToAction("Index");
        }


        // GET: AUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = userAppManager.FindUser(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Access = new SelectList(role.List());
            return View(aspNetUser);
        }

        // POST: AUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,PasswordHash,PhoneNumber")] AspNetUser aspNetUser, string Password, string Access)
        {
            if (ModelState.IsValid)
            {
                AspNetUser aspnetuser = userAppManager.FindUser(aspNetUser.Id);
                aspnetuser.Email = aspNetUser.Email;
                aspnetuser.PhoneNumber = aspNetUser.PhoneNumber;
                aspnetuser.PasswordHash = GetManager().PasswordHasher.HashPassword(Password);
                aspnetuser.AspNetUserRole.RoleId = role.Get(null, Access);
                userAppManager.EditUser(aspnetuser);

                return RedirectToAction("Index");
            }
            //
            ViewBag.Id = new SelectList(userAppManager.FindAllUserRole(), "UserId", "RoleId", aspNetUser.Id);
            return View(aspNetUser);
        }

        // GET: AUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = userAppManager.FindUser(id);
            aspNetUser.AspNetUserRole.RoleId = role.Get(aspNetUser.AspNetUserRole.RoleId, null);

            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id, string RoleId)
        {
            AspNetUser aspNetUser = userAppManager.FindUser(id);
            AspNetUserRole aspNetUserRole = userAppManager.FindAllUserRole().Single(a => a.UserId == aspNetUser.Id);
            userAppManager.DeleteUserRole(aspNetUserRole);
            userAppManager.DeleteUser(aspNetUser);
            return RedirectToAction("Index");
        }


        private ApplicationUserManager GetManager()
        {
            return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
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
