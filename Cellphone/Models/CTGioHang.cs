//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cellphone.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTGioHang
    {
        public int MaGH { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        public Nullable<double> Gia { get; set; }
        public string TrangThai { get; set; }
    
        public virtual GioHang GioHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
