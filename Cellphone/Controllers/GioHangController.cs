using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cellphone.Models;

namespace Cellphone.Controllers
{
    public class GioHangController : Controller
    {
        mobiledbEntities1 db = new mobiledbEntities1();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
       
        public List<BuyProduct> GetProducts()
        {
            List<BuyProduct> gioHang = Session["GioHang"] as List<BuyProduct>;
            if (gioHang == null)
            {
                gioHang = new List<BuyProduct>();
                Session["gioHang"] = gioHang;
            }
            return (gioHang);
        }
        //Thêm một sản phẩm vào giỏ
        [HttpPost]
        public ActionResult AddProduct(int ID)
        {
            var sanPham = db.SanPhams.Find(ID);
            var maKH = (int)Session["MaKH"];

            if (sanPham == null)
            {
                return HttpNotFound();
            }

            //Lấy giỏ hàng hiện tại
            List<BuyProduct> gioHang = GetProducts();

            //Kiểm tra xem có tồn tại mặt hàng trong giỏ không
            //Nếu có thì tăng số lượng lên 1, ngược lại thêm vào giỏ
            BuyProduct product = gioHang.FirstOrDefault(s => s.ID == ID);
            if(product==null)//Sản phẩm chưa có trong giỏ
            {
                product = new BuyProduct(ID);
                product.Soluong = 1;
                product.Giaban = sanPham.GiaBan;
                product.TenSP = sanPham.TenSP;
                gioHang.Add(product);

                //tạo chi tiết giỏ hàng theo mã khách hàng
                db.CTGioHangs.Add(new CTGioHang
                {
                    MaKH = maKH,
                    MaSP = ID,
                    SoLuong = 1,
                    TrangThai = "Chưa xác nhận"
                });
            }
            else
            {
                product.Soluong++; //sản phẩm có trong giỏ thì tăng số lượng lên 1
                                   // Nếu chi tiết giỏ hàng đã tồn tại, cập nhật số lượng

                //Lấy dữ liệu trong db ra (instance)
                var ctGioHang = db.CTGioHangs.FirstOrDefault(c => c.MaKH == maKH && c.MaSP == ID);
                ctGioHang.SoLuong += 1;
                // Sử dụng phương thức Update của DbContext để cập nhật thông tin trong database
                db.Entry(ctGioHang).State = EntityState.Modified;
            }
            //Lưu giỏ hàng vào Session
            Session["GioHang"] = gioHang;

            db.SaveChanges();
            //chuyển về trang chi tiết sản phẩm
            /*return RedirectToAction("DetailsProduct", "Home", new { ID = ID });*/

            return Json(new { success = true });
        }

        //Hàm tính tổng số lượng hàng được mua
        public int TinhTongSL()
        {
            int tongSL = 0;
            List<BuyProduct> gioHang = GetProducts();
            if (gioHang != null)
                tongSL = gioHang.Sum(sp => sp.Soluong);
            return tongSL;
        }

        //Hàm tính tổng tiền của sản phẩm được mua
        private double TinhTongTien()
        {
            double TongTien = 0;
            List<BuyProduct> gioHang = GetProducts();
            if (gioHang != null)
                TongTien = gioHang.Sum(sp => sp.ThanhTien());
            return TongTien;
        }
        
        public ActionResult HienThiGioHang()
        {
            List<BuyProduct> gioHang = GetProducts();

            //Nếu giỏ hàng trống thì trả về trang ban đầu
            if(gioHang==null || gioHang.Count == 0)
            {
                return RedirectToAction("Index", "GioHang");
            }
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(gioHang); //Trả về view hiển thị thoogn tin giỏ hàng
        }
    }


}