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

        public ActionResult getSideBar()
        {
            bool isAdmin = true;
            if (isAdmin) return PartialView("_AdminSideBar");
            else return View();
        }
    }
}