using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cellphone.Models
{
    public class User_KhachHang
    {
        public User user { get; set; }
        public KhachHang customer { get; set; }
    }
}