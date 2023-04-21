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
using PagedList;

namespace Cellphone.Controllers
{
    public class SanPhamController : Controller
    {
        private mobiledbEntities1 db = new mobiledbEntities1();

        public string getImage(int ID) {
            var sanPham = db.SanPhams.Find(ID);
            if (sanPham.HinhAnh != null) return "/Images/Product/" + sanPham.HinhAnh;
            else return "/Images/Product/default.png";
        }

        public string getName(int ID) {
            var sanPham = db.SanPhams.Find(ID);
            return sanPham.TenSP;
        }

        // GET: SanPham
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.Hang1).Include(s => s.LoaiSP1);
            ViewBag.Hang = new SelectList(db.Hangs, "ID", "TenHang");
            ViewBag.LoaiSP = new SelectList(db.LoaiSPs, "ID", "TenLoai");
            return View(sanPhams.ToList());
        }

        public List<SanPham> GetProduct(int? cateId, int brandId = 0, int limit = 12, int page = 1)
        {
            var products = new List<SanPham>();
            if(brandId == 0) products = db.SanPhams.Where(s => s.LoaiSP == cateId).ToList();
            else products = db.SanPhams.Where(s => s.LoaiSP == cateId && s.Hang == brandId).ToList();

            products = products.Skip((page - 1) * limit).Take(limit).ToList();
            return products;
        }

        public ActionResult ShoppingList(int cateId, int? brandId, int? page)
        {
            //phan trang
            int pageSize = 12;
            int pageNum = (page ?? 1);
            int brandid = (brandId ?? 0);

            var hangs = db.Hangs.ToList();
            var loaiSP = db.LoaiSPs.Find(cateId);
            var sanPhams = GetProduct(cateId, brandid, pageSize, pageNum);
            var length = db.SanPhams.ToList().Count();

            var sanPhamListModelView = new SanPhamListModelView {
                Hangs = hangs,
                LoaiSP = loaiSP,
                SanPhams = sanPhams,
                soLuongSP = length
            };

            return View(sanPhamListModelView);
        }

        // GET: SanPham/Details/5
        //thaomy đã tạo cái này
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            //san pham lien quan
            var relatedProduct = db.SanPhams.Where(s => s.LoaiSP == sanPham.LoaiSP && s.Hang == sanPham.Hang).Take(3).ToList();
            var loaiSP = db.LoaiSPs.FirstOrDefault(s => s.ID == sanPham.LoaiSP);
            var sanPhamDetailsModelView = new SanPhamDetailsModelView {
                relatedProduct = relatedProduct,
                product = sanPham,
                productType = loaiSP
            };

            return View(sanPhamDetailsModelView);
        }

        public ActionResult ThongTinSanPham(int? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPham/Create
        public ActionResult Create()
        {
            ViewBag.Hang = new SelectList(db.Hangs, "ID", "TenHang");
            ViewBag.LoaiSP = new SelectList(db.LoaiSPs, "ID", "TenLoai");
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenSP,GiaBan,GiaMua,LoaiSP,Hang,TrangThai")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["HinhAnh"];
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Images/Product/"), fileName);
                    //Lưu tên
                    sanPham.HinhAnh = fileName;
                    //Save vào Images Folder
                    file.SaveAs(path);
                }

                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sanPham);
        }

        // GET: SanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.Hang = new SelectList(db.Hangs, "ID", "TenHang", sanPham.Hang);
            ViewBag.LoaiSP = new SelectList(db.LoaiSPs, "ID", "TenLoai", sanPham.LoaiSP);
            return View(sanPham);
        }

        // POST: SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenSP,GiaBan,GiaMua,SoLuong,LoaiSP,Hang,TrangThai")] SanPham sanPham,
            FormCollection form)
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
                        var path = Path.Combine(Server.MapPath("~/Images/Product/"), fileName);
                        //Lưu tên
                        sanPham.HinhAnh = fileName;
                        //Save vào Images Folder
                        file.SaveAs(path);
                    }
                    else
                    {
                        var hinhanhsrc = form["HinhAnhSrc"];
                        sanPham.HinhAnh = hinhanhsrc;
                    }
                }

                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Hang = new SelectList(db.Hangs, "ID", "TenHang", sanPham.Hang);
            ViewBag.LoaiSP = new SelectList(db.LoaiSPs, "ID", "TenLoai", sanPham.LoaiSP);
            return View(sanPham);
        }

        // GET: SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
