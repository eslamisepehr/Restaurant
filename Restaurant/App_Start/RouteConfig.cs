using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Restaurant
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{Controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = "(Home|Account|Panel)" }
            );

            routes.MapRoute(
                name: "User",
                url: "{Controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = "(Home|Account|User|Foods|Order|Track)" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "Admin/{action}",
                defaults: new { controller = "Admin", action = "Index" , id = UrlParameter.Optional },
                constraints: new { controller = "(Admin)" }
            );

            routes.MapRoute(
                name: "AdminPanel",
                url: "Admin/Panel/{Controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = "(Admin|ACouriers|ADiscountCodes|AFoods|AGetInPlaces|AInfo|AOrderLists|AOrders|AOrderStatus|AOrderTypes|ARestaurantTables|AUserAddresses|AUserRoles|AUsers)" }
            );

            routes.MapRoute(
                name: "Api",
                url: "Api/{Controller}/{action}/{id}",
                defaults: new { controller = "BInfo", action = "Info", id = UrlParameter.Optional },
                constraints: new { controller = "(BInfo|BTrack|BUserAccount)" }
            );



        }
    }
}
