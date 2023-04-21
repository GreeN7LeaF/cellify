using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cellphone.Models
{
    public class UserModelView
    {
        public KhachHang KhachHang;
        public User User;

        public UserModelView() {
            KhachHang = new KhachHang();
            User = new User();
        }
    }
}