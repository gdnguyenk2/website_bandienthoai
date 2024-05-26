using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webbandienthoai.Models
{

    [MetadataTypeAttribute(typeof(NhaCungCapMetadata))]
    public partial class NhaCungCap
    {
        internal sealed class NhaCungCapMetadata
        {
            public int MaNCC { get; set; }
            [DisplayName("Tên nhà cung cấp")]
            [Required(ErrorMessage ="Tên nhà cung cấp không được để trống!")]
            public string TenNCC { get; set; }
            [DisplayName("Tên địa chỉ")]
            [Required(ErrorMessage = "Địa chỉ không được để trống!")]
            public string DiaChi { get; set; }
            [DisplayName("Email")]
            [Required(ErrorMessage = "Email không được để trống!")]
            public string Email { get; set; }
            [DisplayName("Số điện thoại")]
            [Required(ErrorMessage = "Số điện thoại không được để trống!")]
            public string SoDienThoai { get; set; }
            public string Fax { get; set; }
        }
    }
}