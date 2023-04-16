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
                var check = db.Users.FirstOrDefault(x => x.C_Email == user.C_Email);
                if (check == null)
                {
                    user.C_Password = GetMD5(user.MatKhau);
                    user.C_is_Admin = false;
                    user.C_is_Active = true;
                    user.CreateDate = DateTime.Now;
                    user.UpdatedDate = DateTime.Now;
                    db.tb_Customer.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Tài khoản đã tồn tại";
                    return this.Register();
                }
            }
            ViewBag.error = "Tạo tài khoản thất bại xin vui lòng thử lại";
            return this.Register();
        }
        public static string GetMD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {
                sbHash.Append(String.Format("{0:x2}", b));
            }
            return sbHash.ToString();
        }
    }
}