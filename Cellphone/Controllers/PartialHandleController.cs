using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cellphone.Controllers
{
    public class PartialHandleController : Controller
    {
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

        public ActionResult getBreadcumb()
        {
            return PartialView("_Breadcumb");
        }
    }
}