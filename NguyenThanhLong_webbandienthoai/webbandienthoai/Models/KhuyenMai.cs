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

    public partial class KhuyenMai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhuyenMai()
        {
            this.SanPhams = new HashSet<SanPham>();
        }

        public int MaKhuyenMai { get; set; }
        [Required(ErrorMessage = "Tên khuyến mãi không được để trống!")]
        [DisplayName("Tên Khuyến Mãi")]
        public string TenKhuyenMai { get; set; }
        [DisplayName("Mô Tả")]
        public string MoTa { get; set; }
        [Required(ErrorMessage = "Phần trăm giảm giá không được để trống!")]
        [DisplayName("Phần Trăm Khuyến Mãi")]
        public int PhanTramGiamGia { get; set; }
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống!")]
        [DisplayName("Ngày Bắt Đầu")]
        public Nullable<System.DateTime> NgayBatDau { get; set; }
        [Required(ErrorMessage = "Ngày kết thúc không được để trống!")]
        [DisplayName("Ngày Kết Thúc")]
        public Nullable<System.DateTime> NgayKetThuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
