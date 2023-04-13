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
    public class KhuyenMaiController : Controller
    {
        private mobiledbEntities2 db = new mobiledbEntities2();

        // GET: KhuyenMai
        public ActionResult Khuyenmai()
        {
            //ViewBag.KhuyenMai = db.KhuyenMais;
            //return View();
            var khuyenMai = db.KhuyenMais.ToList();
            return View(khuyenMai);

        }

        // GET: KhuyenMai/Details/5
        public ActionResult Details(int id)
        {
            var khuyenMai = db.KhuyenMais.Where(c => c.ID == id).FirstOrDefault();
            return View(khuyenMai);
        }

        // GET: KhuyenMai/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhuyenMai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create( KhuyenMai khuyenMai)
        {
            try
            {
                db.KhuyenMais.Add(khuyenMai);
                 db.SaveChanges();
                return RedirectToAction("Khuyenmai");
            }
            catch { return Content("Lỗi tạo mới"); }

           
        }

        // GET: KhuyenMai/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var khuyenMai = db.KhuyenMais.Where(c => c.ID == id).FirstOrDefault();
            return View(khuyenMai);


        }
        // POST: KhuyenMai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Edit( KhuyenMai Khuyenmai)
        {
            db.Entry(Khuyenmai).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Khuyenmai");
        }
        [HttpGet]
        // GET: KhuyenMai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var khuyenMai = db.KhuyenMais.Where(c => c.ID == id).FirstOrDefault();
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // POST: KhuyenMai/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            db.KhuyenMais.Remove(khuyenMai);
            db.SaveChanges();
            return RedirectToAction("KhuyenMai");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
