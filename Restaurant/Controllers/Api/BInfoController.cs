using Restaurant.AppManagers.MainModules;
using Restaurant.Helper.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Controllers.Api
{
    public class BInfoController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();
        Api_Info apiInfo = new Api_Info();

        // GET: Main
        public JsonResult Info()
        {
            var list = userAppManager.FindInfo(1);
            Api_Info apiInfo = new Api_Info()
            {
                Count = list.Count.ToString(),
                ContactUs = list.ContactUs,
                About = list.About,
                //PictureLink = list.PictureLink,
                PictureLink = "http://upload.eslamisepehr.com/Restaurant/Mobile/Restaurant-Bg.jpg",
                PhoneNumber = "02188737624"
            };
            return Json(apiInfo, JsonRequestBehavior.AllowGet);
        }
    }
}