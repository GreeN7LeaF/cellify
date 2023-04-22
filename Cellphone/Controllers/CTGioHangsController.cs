using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
            if (Session["MaKH"] == null) return Redirect("/Login");
            var maKH = (int)Session["MaKH"];
            var cTGioHangs = db.CTGioHangs.Where(s => s.MaKH == maKH);
            var donHangView = new DonHangModelView {
            };

            return View(cTGioHangs.ToList());
        }

        public ActionResult SideCart()
        {
            if (Session["MaKH"] == null) return PartialView("PP_GioHangCart");
            var maKH = (int)Session["MaKH"];
            var cTGioHangs = db.CTGioHangs.Where(s => s.MaKH == maKH);
            if (cTGioHangs == null) {
                return PartialView("PP_GioHangCart", new List<CTGioHang>());
            }
            return PartialView("PP_GioHangCart", cTGioHangs.ToList());
        }
        public ActionResult Clear()
        {
            var maKH = (int)Session["MaKH"];
            //xóa sản phẩm trong giỏ hàng 
            string sql = "DELETE FROM CTGioHang WHERE MaKH = @maKH";
            db.Database.ExecuteSqlCommand(sql, new SqlParameter("@maKH", maKH));
            return RedirectToAction("Index");
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
        /*public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen");
            ViewBag.MaSP = new SelectList(db.SanPhams, "ID", "TenSP");
            return View();
        }*/

        // POST: CTGioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(int ID)
        {
            if (ModelState.IsValid)
            {
                var sanPham = db.SanPhams.Find(ID);

                if (Session["MaKH"] == null) {
                    return Json(new { error = true });
                }

                var maKH = (int)Session["MaKH"];

                if (sanPham == null)
                {
                    return HttpNotFound();
                }

                var ct = db.CTGioHangs.FirstOrDefault(s => s.MaKH == maKH && s.MaSP == ID);
                if(ct == null)
                {
                    ct = new CTGioHang();
                    // add new
                    ct.MaKH = maKH;
                    ct.MaSP = ID;
                    ct.TenSP = sanPham.TenSP;
                    ct.SoLuong = 1;
                    ct.Gia = sanPham.GiaBan;
                    ct.ThanhTien = sanPham.GiaBan;
                    ct.TrangThai = "0";

                    db.CTGioHangs.Add(ct);
                } else
                {
                    ct.SoLuong++;
                    ct.ThanhTien = sanPham.GiaBan * ct.SoLuong;
                }

                db.SaveChanges();
                return Json(new { success = true, });
                /*return Redirect("/home/index");*/
            }
            return Json(new { error = true });
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
        /*public ActionResult Delete(int? id)
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
        }*/

        // POST: CTGioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            try { 
                var maKH = Session["MaKH"];
                CTGioHang cTGioHang = db.CTGioHangs.FirstOrDefault(s => s.MaKH == (int)maKH && s.MaSP == ID);
                db.CTGioHangs.Remove(cTGioHang);
                db.SaveChanges();
                /*return Json(new { success = true });*/
                return Redirect("/home/index");
            } catch(Exception exc)
            {
                return Json(new { error = true });
            }
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
