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
    
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            this.BaoHanhs = new HashSet<BaoHanh>();
            this.DonHangs = new HashSet<DonHang>();
            this.DanhGias = new HashSet<DanhGia>();
        }
    
        public int ID { get; set; }
        public int MaKH { get; set; }
        public Nullable<int> MaGH { get; set; }
        public int LoaiKH { get; set; }
        public string HoTen { get; set; }
        public string HinhAnh { get; set; }
        public string DiaChi { get; set; }
        public int GioiTinh { get; set; }
        public string DienThoai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaoHanh> BaoHanhs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }
        public virtual GioHang GioHang { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }
        public virtual LoaiKH LoaiKH1 { get; set; }
        public virtual User User { get; set; }
    }
}
