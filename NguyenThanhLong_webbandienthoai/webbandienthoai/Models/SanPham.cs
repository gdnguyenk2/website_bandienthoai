//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webbandienthoai.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.BinhLuans = new HashSet<BinhLuan>();
            this.ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
            this.ChiTietPhieuNhaps = new HashSet<ChiTietPhieuNhap>();
            this.ChiTietGioHangs = new HashSet<ChiTietGioHang>();
        }
    
        public int MaSP { get; set; }
        public int MaNCC { get; set; }
        public int MaNSX { get; set; }
        public int MaLoaiSP { get; set; }
        public Nullable<int> MaKhuyenMai { get; set; }
        public string TenSP { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public string MoTa { get; set; }
        public string CauHinh { get; set; }
        public string HinhAnh { get; set; }
        public string HinhAnh2 { get; set; }
        public string HinhAnh3 { get; set; }
        public string HinhAnh4 { get; set; }
        public int SoLuongTon { get; set; }
        public Nullable<int> LuotXem { get; set; }
        public Nullable<int> LuotBinhChon { get; set; }
        public Nullable<int> LuotBinhLuan { get; set; }
        public Nullable<int> SoLanMua { get; set; }
        public bool Moi { get; set; }
        public bool BanChay { get; set; }
        public bool DaXoa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public virtual KhuyenMai KhuyenMai { get; set; }
        public virtual LoaiSanPham LoaiSanPham { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
        public virtual NhaSanXuat NhaSanXuat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }
    }
}
