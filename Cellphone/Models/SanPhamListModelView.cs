using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cellphone.Models
{
    public class SanPhamListModelView
    {
        public List<SanPham> SanPhams;
        public List<LoaiSP> LoaiSPs;
        public string tenLoaiSP;
        public List<Hang> Hangs;

        public SanPhamListModelView()
        {
            List<SanPham> SanPhams = new List<SanPham>();
            List<LoaiSP> LoaiSPs = new List<LoaiSP>();
            List<Hang> Hangs = new List<Hang>();
            tenLoaiSP = "";
        }
    }
}