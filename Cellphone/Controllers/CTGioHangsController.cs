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
    public class CTGioHangsController : Controller
    {
        private mobiledbEntities1 db = new mobiledbEntities1();

        // GET: CTGioHangs
        public ActionResult Index()
        {
            var cTGioHangs = db.CTGioHangs.Include(c => c.KhachHang).Include(c => c.SanPham);
            return View(cTGioHangs.ToList());
        }

        // GET: CTGioHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTGioHang cTGioHang = db.CTGioHangs.Find(id);
            if (cTGioHang == null)
            {
                return HttpNotFound();
            }
            return View(cTGioHang);
        }

        // GET: CTGioHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen");
            ViewBag.MaSP = new SelectList(db.SanPhams, "ID", "TenSP");
            return View();
        }

        // POST: CTGioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,SoLuong,Gia,TrangThai,MaKH")] CTGioHang cTGioHang)
        {
            if (ModelState.IsValid)
            {
                db.CTGioHangs.Add(cTGioHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", cTGioHang.MaKH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "ID", "TenSP", cTGioHang.MaSP);
            return View(cTGioHang);
        }

        // GET: CTGioHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTGioHang cTGioHang = db.CTGioHangs.Find(id);
            if (cTGioHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", cTGioHang.MaKH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "ID", "TenSP", cTGioHang.MaSP);
            return View(cTGioHang);
        }

        // POST: CTGioHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,SoLuong,Gia,TrangThai,MaKH")] CTGioHang cTGioHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTGioHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", cTGioHang.MaKH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "ID", "TenSP", cTGioHang.MaSP);
            return View(cTGioHang);
        }

        // GET: CTGioHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTGioHang cTGioHang = db.CTGioHangs.Find(id);
            if (cTGioHang == null)
            {
                return HttpNotFound();
            }
            return View(cTGioHang);
        }

        // POST: CTGioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTGioHang cTGioHang = db.CTGioHangs.Find(id);
            db.CTGioHangs.Remove(cTGioHang);
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
