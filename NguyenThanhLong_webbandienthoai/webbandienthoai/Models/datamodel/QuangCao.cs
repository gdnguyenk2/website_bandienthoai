using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webbandienthoai.Models
{

    [MetadataTypeAttribute(typeof(QuangCaoMetadata))]
    public partial class QuangCao
    {
        internal sealed class QuangCaoMetadata
        {
            public int MaQuangCao { get; set; }
            [DisplayName("Tên Quảng Cáo")]
            [Required(ErrorMessage ="Tên quảng cáo không được để trống!")]
            public string TenQC { get; set; }
            [DisplayName("Hình Ảnh")]
            [Required(ErrorMessage = "Hình ảnh không được để trống!")]
            public string HinhAnh { get; set; }
            [DisplayName("Ngày Cập Nhật")]
            [Required(ErrorMessage = "Ngay cap nhat không được để trống!")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
        }
    }
}