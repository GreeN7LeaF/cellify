﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class mobiledbEntities1 : DbContext
    {
        public mobiledbEntities1()
            : base("name=mobiledbEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BaoHanh> BaoHanhs { get; set; }
        public virtual DbSet<CTDonHang> CTDonHangs { get; set; }
        public virtual DbSet<CTGioHang> CTGioHangs { get; set; }
        public virtual DbSet<CTKhuyenMai> CTKhuyenMais { get; set; }
        public virtual DbSet<CTPhieuNhap> CTPhieuNhaps { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<Hang> Hangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<LoaiKH> LoaiKHs { get; set; }
        public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<DanhGia> DanhGias { get; set; }
    
        public virtual int sp_themDonHang(Nullable<int> makh, Nullable<double> tongtien, string trangthai, Nullable<System.DateTime> ngaycapnhat)
        {
            var makhParameter = makh.HasValue ?
                new ObjectParameter("makh", makh) :
                new ObjectParameter("makh", typeof(int));
    
            var tongtienParameter = tongtien.HasValue ?
                new ObjectParameter("tongtien", tongtien) :
                new ObjectParameter("tongtien", typeof(double));
    
            var trangthaiParameter = trangthai != null ?
                new ObjectParameter("trangthai", trangthai) :
                new ObjectParameter("trangthai", typeof(string));
    
            var ngaycapnhatParameter = ngaycapnhat.HasValue ?
                new ObjectParameter("ngaycapnhat", ngaycapnhat) :
                new ObjectParameter("ngaycapnhat", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_themDonHang", makhParameter, tongtienParameter, trangthaiParameter, ngaycapnhatParameter);
        }
    }
}
