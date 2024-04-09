﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class ThanhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThanhVien()
        {
            this.BinhLuans = new HashSet<BinhLuan>();
            this.GioHangs = new HashSet<GioHang>();
            this.KhachHangs = new HashSet<KhachHang>();
        }
    
        public int MaTV { get; set; }
        public Nullable<int> MaLoaiTV { get; set; }

        [DisplayName("Tài khoản")]
        public string TaiKhoan { get; set; }

        [DisplayName("Mật khẩu")]
        public string MatKhau { get; set; }

        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        public string DiaChi { get; set; }

        
        [DisplayName("Email")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }

        [DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }
        public string CauHoi { get; set; }
        public string CauTraLoi { get; set; }
        public string HinhDaiDien { get; set; }
        public string MaToken { get; set; }
        public Nullable<System.DateTime> ThoiGianMaToken { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
        public virtual LoaiThanhVien LoaiThanhVien { get; set; }
    }
}
