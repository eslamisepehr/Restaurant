using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Restaurant.AppManagers.MainModules;
using Restaurant.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Controllers.User
{
    [Authorize]
    public class PanelController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        // GET: Panel
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "CurrentPassword")] string CurrentPassword, [Bind(Include = "NewPassword")] string NewPassword, [Bind(Include = "NewPasswordConfirm")] string NewPasswordConfirm)
        {
            if (ModelState.IsValid)
            {
                if (CurrentPassword != null && NewPassword != null && NewPasswordConfirm != null && NewPassword == NewPasswordConfirm)
                {
                    ViewData["Message"] = "گذرواژه شما با موفقیت تغییر یافت!";
                    GetManager().ChangePassword(User.Identity.GetUserId(), CurrentPassword, NewPassword);
                }
            }
            return View();
        }

        #region Address
        public ActionResult Address()
        {
            var list = userAppManager.FindAllUserAddress().Where(a => a.UserId == User.Identity.GetUserId()).OrderByDescending(a => a.Id);
            return View(list);
        }

        public ActionResult CreateAddress()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAddress([Bind(Include = "Place,Unit,Floor,Number,Telephone")] UserAddress userAddress)
        {
            if (ModelState.IsValid)
            {
                userAddress.UserId = User.Identity.GetUserId();
                userAppManager.AddUserAddress(userAddress);
                return RedirectToAction("Address");
            }
            return View();
        }


        public ActionResult EditAddress(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAddress userAddress = userAppManager.FindAllUserAddress().Single(a => a.Id == id && a.UserId == User.Identity.GetUserId());
            if (userAddress == null)
            {
                return HttpNotFound();
            }
            return View(userAddress);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAddress([Bind(Include = "Id,UserId,Place,Unit,Floor,Number,Telephone")] UserAddress userAddress)
        {
            if (ModelState.IsValid)
            {
                UserAddress list = userAppManager.FindAllUserAddress().Single(a => a.Id == userAddress.Id && a.AspNetUser.Id == User.Identity.GetUserId());
                if (list != null)
                {
                    list.Place = userAddress.Place;
                    list.Unit = userAddress.Unit;
                    list.Floor = userAddress.Floor;
                    list.Number = userAddress.Number;
                    list.Telephone = userAddress.Telephone;
                    userAppManager.EditUserAddress(list);
                    return RedirectToAction("Address");
                }
            }
            return View(userAddress);
        }


        //public ActionResult DeleteAddress(int id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserAddress userAddress = userAppManager.FindAllUserAddress().Single(a => a.Id == id && a.UserId == User.Identity.GetUserId());
        //    if (userAddress == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userAddress);
        //}

        //// POST: UserAddresses/Delete/5
        //[HttpPost, ActionName("DeleteAddress")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmedAddress(int id)
        //{
        //    var list = userAppManager.FindAllUserAddress().Single(a => a.Id == id && a.UserId == User.Identity.GetUserId());
        //    if (list != null)
        //    {
        //        UserAddress userAddress = userAppManager.FindAllUserAddress().Single(a => a.Id == id && a.UserId == User.Identity.GetUserId());
        //        userAppManager.DeleteUserAddress(userAddress);
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        #endregion

        private ApplicationUserManager GetManager()
        {
            return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }
    }
}