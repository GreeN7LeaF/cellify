using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

        // GET: SanPham/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HoTen, DienThoai, DiaChi")] KhachHang khachHang, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["HinhAnh"];
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    if (fileName != "")
                    {
                        //Tạo đường dẫn tới file
                        var path = Path.Combine(Server.MapPath("~/Images/User/"), fileName);
                        //Lưu tên
                        khachHang.HinhAnh = fileName;
                        //Save vào Images Folder
                        file.SaveAs(path);
                    }
                    else
                    {
                        var hinhanhsrc = form["HinhAnhSrc"];
                        khachHang.HinhAnh = hinhanhsrc;
                    }
                }
                khachHang.ID = (int)Session["ID"];
                khachHang.LoaiKH = (int)Session["LoaiKH"];
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromDonHang([Bind(Include = "HoTen, DienThoai, DiaChi")] KhachHang khachHang, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["HinhAnh"];
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    if (fileName != "")
                    {
                        //Tạo đường dẫn tới file
                        var path = Path.Combine(Server.MapPath("~/Images/User/"), fileName);
                        //Lưu tên
                        khachHang.HinhAnh = fileName;
                        //Save vào Images Folder
                        file.SaveAs(path);
                    }
                    else
                    {
                        var hinhanhsrc = form["HinhAnhSrc"];
                        khachHang.HinhAnh = hinhanhsrc;
                    }
                }
                khachHang.ID = (int)Session["ID"];
                khachHang.LoaiKH = (int)Session["LoaiKH"];
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachHang);
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
        public ActionResult Edit([Bind(Include = "HoTen,DienThoai,DiaChi")]  KhachHang kh, FormCollection form)
        {
            var file = Request.Files["HinhAnh"];
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                if (fileName != "")
                {
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Images/User/"), fileName);
                    //Lưu tên
                    kh.HinhAnh = fileName;
                    //Save vào Images Folder
                    file.SaveAs(path);
                }
                else
                {
                    var hinhanhsrc = form["HinhAnhSrc"];
                    kh.HinhAnh = hinhanhsrc;
                }
            }

            db.Entry(kh).State = (System.Data.Entity.EntityState)System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CustomerManagement");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFromDonHang([Bind(Include = "MaKH,HoTen,DienThoai,DiaChi")] KhachHang kh, FormCollection form)
        {
            var file = Request.Files["HinhAnh"];
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                if (fileName != "")
                {
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Images/User/"), fileName);
                    //Lưu tên
                    kh.HinhAnh = fileName;
                    //Save vào Images Folder
                    file.SaveAs(path);
                }
                else
                {
                    var hinhanhsrc = form["HinhAnhSrc"];
                    kh.HinhAnh = hinhanhsrc;
                }
            }
            kh.ID = (int)Session["ID"];
            kh.LoaiKH = (int)Session["LoaiKH"];
            db.Entry(kh).State = (System.Data.Entity.EntityState)System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Redirect("/DonHang/Create");
        }
    }
}