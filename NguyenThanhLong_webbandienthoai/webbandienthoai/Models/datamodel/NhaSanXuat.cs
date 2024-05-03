using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webbandienthoai.Models
{

    [MetadataTypeAttribute(typeof(NhaSanXuatMetadata))]
    public partial class NhaSanXuat
    {
        internal sealed class NhaSanXuatMetadata
        {
            public int MaNSX { get; set; }
            [DisplayName("Tên nhà sản xuất")]
            [Required(ErrorMessage = "Tên nhà sản xuất không được để trống!")]
            public string TenNSX { get; set; }
            [DisplayName("Thông Tin")]
            [Required(ErrorMessage = "Thông tin không được để trống!")]
            public string ThongTin { get; set; }
            [DisplayName("Logo")]
            [Required(ErrorMessage = "Logo không được để trống!")]
            public string Logo { get; set; }
        }
    }
}