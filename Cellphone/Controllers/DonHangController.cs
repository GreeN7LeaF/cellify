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
    public class DonHangController : Controller
    {
        private mobiledbEntities1 db = new mobiledbEntities1();

        // GET: DonHang
        public ActionResult Index()
        {
            var donHangs = db.DonHangs.Include(d => d.KhachHang);
            return View(donHangs.ToList());
        }
        public ActionResult ThankYou()
        {
            var maKH = (int)Session["MaKH"];
            var khachhang = db.KhachHangs.Find(maKH);
            return View(khachhang);
        }
        public ActionResult HienThiDonHang()
        {
            if (Session["MaKH"] == null) return Redirect("/Login");

            var maKH = (int)Session["MaKH"];
            var donHangs = db.DonHangs.Where(s => s.MaKH == maKH).ToList();
            return View(donHangs);
        }

        // GET: DonHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }

            var maKH = donHang.MaKH;
            var khachHang = db.KhachHangs.Find(maKH);
            var ctDonHang = db.CTDonHangs.Where(s => s.MaDH == id).ToList();
            var donHangModel = new DonHangModelView {
                KhachHang = khachHang,
                CTDonHang = ctDonHang,
                DonHang = donHang
            };

            return View(donHangModel);
        }

        // GET: DonHang/Create
        public ActionResult Create()
        {
            if (Session["MaKH"] == null) return Redirect("/Login");

            var maKH = (int)Session["MaKH"];
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen");
            var khachHang = db.KhachHangs.Find(maKH);
            var ctGioHang = db.CTGioHangs.Where(s => s.MaKH == maKH).ToList();

            var donHang = new DonHang();
            donHang.TongTien = db.CTGioHangs.Where(c => c.MaKH == maKH)
                            .Sum(c => c.ThanhTien);

            var viewmodel = new DonHangModelView
            {
                KhachHang = khachHang,
                CTGioHangs = ctGioHang,
                DonHang = donHang
            };
            return View(viewmodel);
        }

        // POST: DonHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TongTien")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                var maKH = (int)Session["MaKH"];
                var ctGioHang = db.CTGioHangs.Where(s => s.MaKH == maKH).ToList();

                donHang.TrangThai = "Chưa xác nhận";
                donHang.MaKH = maKH;
                donHang.NgayLap = DateTime.Now;
                donHang.TongTien = db.CTGioHangs.Where(c => c.MaKH == maKH)
                            .Sum(c => c.ThanhTien);
                donHang.GiamGia = 0; // tạm thời chưa làm giảm giá
                donHang.ThanhTien = donHang.TongTien - donHang.GiamGia;

                db.DonHangs.Add(donHang);
                db.SaveChanges();

                //lấy id của đơn hàng vừa tạo
                int maDH = donHang.ID;
                //lưu chi tiết đơn hàng
                foreach (var item in ctGioHang) {
                    var ctDonHang = new CTDonHang {
                        MaDH = maDH,
                        MaSP = item.MaSP,
                        SoLuong = item.SoLuong,
                        Gia = item.ThanhTien
                    };
                    db.CTDonHangs.Add(ctDonHang);
                }
                db.SaveChanges();

                //xóa sản phẩm trong giỏ hàng 
                string sql = "DELETE FROM CTGioHang WHERE MaKH = @maKH";
                db.Database.ExecuteSqlCommand(sql, new SqlParameter("@maKH", maKH));
                return RedirectToAction("ThankYou");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", donHang.MaKH);
            return View(donHang);
        }

        // GET: DonHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", donHang.MaKH);
            return View(donHang);
        }

        // POST: DonHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaKH,NgayLap,TongTien,GiamGia,TrangThai")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", donHang.MaKH);
            return View(donHang);
        }

        public ActionResult UpdateStatus(int maDH)
        {
            var donHang = db.DonHangs.Find(maDH);
            var status = donHang.TrangThai;
            if (status == "Chưa xác nhận") donHang.TrangThai = "Đã xác nhận";
            else donHang.TrangThai = "Hoàn thành";
            db.Entry(donHang).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = maDH});
        }

        // GET: DonHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
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
