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
    public class CTKhuyenMaiController : Controller
    {
        private mobiledbEntities1 db = new mobiledbEntities1();

        // GET: CTKhuyenMai
        public ActionResult Index()
        {
            var cTKhuyenMais = db.CTKhuyenMais.Include(c => c.KhuyenMai).Include(c => c.SanPham);
            return View(cTKhuyenMais.ToList());
        }

        // GET: CTKhuyenMai/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTKhuyenMai cTKhuyenMai = db.CTKhuyenMais.Find(id);
            if (cTKhuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(cTKhuyenMai);
        }

        // GET: CTKhuyenMai/Create
        public ActionResult Create()
        {
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "ID", "TenKM");
            ViewBag.MaSP = new SelectList(db.SanPhams, "ID", "TenSP");
            return View();
        }

        // POST: CTKhuyenMai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaSP,MaKM,Gia,SoLuong,TrangThai")] CTKhuyenMai cTKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.CTKhuyenMais.Add(cTKhuyenMai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKM = new SelectList(db.KhuyenMais, "ID", "TenKM", cTKhuyenMai.MaKM);
            ViewBag.MaSP = new SelectList(db.SanPhams, "ID", "TenSP", cTKhuyenMai.MaSP);
            return View(cTKhuyenMai);
        }

        // GET: CTKhuyenMai/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTKhuyenMai cTKhuyenMai = db.CTKhuyenMais.Find(id);
            if (cTKhuyenMai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "ID", "TenKM", cTKhuyenMai.MaKM);
            ViewBag.MaSP = new SelectList(db.SanPhams, "ID", "TenSP", cTKhuyenMai.MaSP);
            return View(cTKhuyenMai);
        }

        // POST: CTKhuyenMai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaSP,MaKM,Gia,SoLuong,TrangThai")] CTKhuyenMai cTKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTKhuyenMai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "ID", "TenKM", cTKhuyenMai.MaKM);
            ViewBag.MaSP = new SelectList(db.SanPhams, "ID", "TenSP", cTKhuyenMai.MaSP);
            return View(cTKhuyenMai);
        }

        // GET: CTKhuyenMai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTKhuyenMai cTKhuyenMai = db.CTKhuyenMais.Find(id);
            if (cTKhuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(cTKhuyenMai);
        }

        // POST: CTKhuyenMai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTKhuyenMai cTKhuyenMai = db.CTKhuyenMais.Find(id);
            db.CTKhuyenMais.Remove(cTKhuyenMai);
            db.SaveChanges();
            return RedirectToAction("Index");
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
