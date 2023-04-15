using Cellphone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cellphone.Controllers
{
    public class PartialHandleController : Controller
    {
        private mobiledbEntities1 db = new mobiledbEntities1();

        // GET: PartialHandle
        public ActionResult getHeaderNav()
        {
            bool isAdmin = true;
            if (isAdmin) return PartialView("_AuthHeader");
            else return View();
        }

        public ActionResult getSideBar(int headerId)
        {
            bool isAdmin = true;
            if (isAdmin) {
                switch (headerId) {
                    case 2:
                        return PartialView("_SanPhamSideBar");
                    case 3:
                        return PartialView("_CuaHangSideBar");
                    default:
                        return PartialView("_EmptyPartialPage");
                }
            }
            else return PartialView("_EmptyPartialPage");
        }

        public ActionResult getProductTab(int id)
        {
            try
            {
                var product = db.SanPhams
                           .Where(p => p.LoaiSP == id)
                           .ToList();

                return PartialView("PP_SanPhamCart", product);
            }
            catch (Exception ex) { 
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult getBreadcumb()
        {
            return PartialView("_Breadcumb");
        }
    }
}