using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cellphone.Models
{
    public class SanPhamDetailsModelView
    {
        public List<SanPham> relatedProduct;
        public List<DanhGia> reviews;
        public SanPham product;
        public LoaiSP productType;

        public SanPhamDetailsModelView()
        {
            relatedProduct = new List<SanPham>();
            product = new SanPham();
            productType = new LoaiSP();
            reviews = new List<DanhGia>();
        }
    }
}