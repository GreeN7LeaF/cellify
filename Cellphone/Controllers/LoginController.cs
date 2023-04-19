using Cellphone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cellphone.Controllers
{
    public class LoginController : Controller
    {
        private mobiledbEntities1 db = new mobiledbEntities1();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string C_Email, string password)
        {
            if (ModelState.IsValid)
            {
                //mã hóa mật khẩu
                var Password = GetSHA256(password);
                var user = db.Users.SingleOrDefault(x => x.C_Email.Equals(C_Email) && x.C_Password.Equals(Password));
                try { 
                    if (user != null){
                        var customer = db.KhachHangs.SingleOrDefault(x => x.ID.Equals(user.C_ID));
                        if (customer != null)
                        {
                            if (user.C_is_Active == true)
                            {
                                //lưu vào session để kierm tra thông tin đăng nhập
                                Session["ID"] = user.C_ID;
                                Session["Email"] = user.C_Email;
                                Session["MaKH"] = customer.MaKH;
                                Session["HoTen"] = customer.HoTen;
                                Session["IsLoggedIn"] = true;

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                TempData["ToastMessage"] = "Tài khoản của bạn đã bị khóa";
                                ViewBag.error = "Tài khoản của bạn đã bị khóa";
                                return RedirectToAction("Login", "Login");
                            }
                        }
                        else
                        {
                            KhachHang newCustomer = new KhachHang();
                            newCustomer.ID = db.Users.FirstOrDefault(x => x.C_Email == user.C_Email).C_ID;
                            newCustomer.LoaiKH = 1;
                            newCustomer.HoTen = "user-" + newCustomer.ID;
                            db.KhachHangs.Add(newCustomer);
                            db.SaveChanges();

                            var customer1 = db.KhachHangs.SingleOrDefault(x => x.ID.Equals(newCustomer.ID));

                            Session["ID"] = user.C_ID;
                            Session["MaKH"] = customer1.MaKH;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else{
                        TempData["ToastMessage"] = "Tài khoản hoặc mật khẩu không đúng";
                        ViewBag.error = "Tài khoản hoặc mật khẩu không đúng";
                        return RedirectToAction("Login", "Login");

                    }
                } catch(Exception exc)
                {
                    TempData["ToastMessage"] = "Đã có lỗi, vui lòng thử lại";
                    return RedirectToAction("Login", "Login");
                }

            }
            TempData["ToastMessage"] = "Đăng nhập thất bại xin vui lòng thử lại";
            ViewBag.error = "Đăng nhập thất bại xin vui lòng thử lại";
            return this.Login();
        }
        private bool checkAdmin(string email) {
            return true;
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var check = db.Users.FirstOrDefault(x => x.C_Email == user.C_Email);
                    if (check == null)
                    {
                        user.C_Password = GetSHA256(user.C_Password);
                        user.C_is_Admin = 0;
                        user.C_is_Active = true;
                        user.C_Created_at = DateTime.Now;
                        user.C_Updated_at = DateTime.Now;
                        db.Users.Add(user);
                        db.SaveChanges();

                        TempData["ToastMessage"] = "Đăng ký thành công, vui lòng đăng nhập lại!";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["ToastMessage"] = "Tài khoản đã tồn tại";
                        return RedirectToAction("Register","Login");
                    }
                }
                catch (Exception exc)
                {
                    TempData["ToastMessage"] = "Xảy ra lỗi, xin vui lòng thử lại" + exc.Message;
                    return RedirectToAction("Register", "Login");
                }
            }
            TempData["ToastMessage"] = "Tạo tài khoản thất bại xin vui lòng thử lại";
            return RedirectToAction("Register", "Login");
        }
        public static string GetSHA256(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            SHA256Managed sha256hashstring = new SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);
            StringBuilder sbHash = new StringBuilder();
            foreach (byte b in hash)
            {
                sbHash.Append(b.ToString("x2"));
            }
            return sbHash.ToString();
        }
    }
}