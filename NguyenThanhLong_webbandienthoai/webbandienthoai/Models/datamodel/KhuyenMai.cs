using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webbandienthoai.Models
{

    [MetadataTypeAttribute(typeof(KhuyenMaiMetadata))]
    public partial class KhuyenMai
    {
        internal sealed class KhuyenMaiMetadata
        {

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
        }
    }
}