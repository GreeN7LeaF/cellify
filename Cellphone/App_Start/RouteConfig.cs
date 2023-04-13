using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cellphone
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Hang",
                url: "SanPham/Hang/{action}",
                defaults: new { controller = "Hang", action = "Index" }
            );

            routes.MapRoute(
                name: "KhuyenMai",
                url: "SanPham/KhuyenMai/{action}",
                defaults: new { controller = "KhuyenMai", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
