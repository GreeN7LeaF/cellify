using Cellphone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cellphone.Controllers
{
    public class HomeController : Controller
    {
        private mobiledbEntities1 db = new mobiledbEntities1();
        public ActionResult Index()
        {
            var sanphams = db.SanPhams.ToList();
            ViewBag.LoaiSP = db.LoaiSPs.ToList();
            ViewBag.CTGioHang = db.CTGioHangs.ToList();
            return View(sanphams);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CuaHang()
        {
            return View();
        }
    }
}