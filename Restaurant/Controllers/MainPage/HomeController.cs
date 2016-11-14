using Restaurant.AppManagers.MainModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();
        public ActionResult Index()
        {
            var list = userAppManager.FindAllInfo().Where(a => a.Id == 1);
            return View(list);
        }
    }
}