using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cellphone.Models;

namespace Cellphone.Controllers
{
    public class KhachHangController : Controller

    {
        mobiledbEntities1 db = new mobiledbEntities1();
        // GET: KhachHang
        public ActionResult CustomerManagement()
        {
            var ttkh = db.KhachHangs.ToList();
            return View(ttkh);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang kh = db.KhachHangs.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang kh = db.KhachHangs.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(KhachHang kh)
        {
            db.Entry(kh).State = (System.Data.Entity.EntityState)System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CustomerManagement");
        }
    }
}