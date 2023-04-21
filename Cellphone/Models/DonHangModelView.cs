using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cellphone.Models
{
    public class DonHangModelView
    {
        public DonHang DonHang { get; set; }
        public KhachHang KhachHang { get; set; }
        public List<CTGioHang> CTGioHangs { get; set; }
        public List<CTDonHang> CTDonHang { get; set; }

        public DonHangModelView() {
            DonHang DonHang = new DonHang();
            KhachHang KhachHangs = new KhachHang();
            List<CTGioHang> CTGioHangs = new List<CTGioHang>();
            List<CTDonHang> CTDonHang = new List<CTDonHang>();
        }

    }
}