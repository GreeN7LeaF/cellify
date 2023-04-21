using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cellphone.Models
{
    public class SanPhamListModelView
    {
        public List<SanPham> SanPhams;
        public int soLuongSP;
        public LoaiSP LoaiSP;
        public List<Hang> Hangs;

        public SanPhamListModelView()
        {
            List<SanPham> SanPhams = new List<SanPham>();
            LoaiSP = new LoaiSP();
            List<Hang> Hangs = new List<Hang>();
            soLuongSP = SanPhams.Count();
        }
    }
}