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
    public class AUserAddressesController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        // GET: AUserAddresses
        public ActionResult Index(string Email, string PhoneNumber,string Address)
        {
            var list = userAppManager.FindAllUserAddress();

            if (Email != null && Email != "")
            {
                ViewData["Email"] = Email;
                var listByEmail = list.Where(a => a.AspNetUser.Email.Contains(Email));
                return View(listByEmail);
            }
            else if (PhoneNumber != null && PhoneNumber != "")
            {
                ViewData["PhoneNumber"] = PhoneNumber;
                var listByPhoneNumber = list.Where(a => a.AspNetUser.PhoneNumber == PhoneNumber);
                return View(listByPhoneNumber);
            }
            else if (Address != null && Address != "")
            {
                ViewData["Address"] = Address;
                var listByAddress = list.Where(a => a.Place.Contains(Address));
                return View(listByAddress);
            }


            return View(list);
        }

        // GET: AUserAddresses/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAddress userAddress = userAppManager.FindUserAddress(id);
            if (userAddress == null)
            {
                return HttpNotFound();
            }
            return View(userAddress);
        }

        
    }
}
