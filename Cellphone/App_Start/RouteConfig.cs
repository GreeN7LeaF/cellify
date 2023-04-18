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
                name: "KhachHang",
                url: "KhachHang/{action}",
                defaults: new { controller = "KhachHang", action = "Index" }
            );

            routes.MapRoute(
                name: "DonHang",
                url: "CuaHang/DonHang/{action}",
                defaults: new { controller = "DonHang", action = "Index" }
            );

            routes.MapRoute(
                name: "CuaHang",
                url: "CuaHang/",
                defaults: new { controller = "Home", action = "CuaHang" }
            );

            routes.MapRoute(
                name: "SanPham_LoaiSP",
                url: "SanPham/LoaiSP/{action}",
                defaults: new { controller = "LoaiSP", action = "Index" }
            );

            routes.MapRoute(
                name: "SanPham_Hang",
                url: "SanPham/Hang/{action}",
                defaults: new { controller = "Hang", action = "Index" }
            );

            routes.MapRoute(
                name: "SanPham_KhuyenMai",
                url: "SanPham/KhuyenMai/{action}",
                defaults: new { controller = "KhuyenMai", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
