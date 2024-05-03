using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webbandienthoai.Models
{

    [MetadataTypeAttribute(typeof(LoaiSanPhamMetadata))]
    public partial class LoaiSanPham
    {
        internal sealed class LoaiSanPhamMetadata
        {
            public int MaLoaiSP { get; set; }
            [Required(ErrorMessage = "Tên danh mục không được để trống!")]
            [DisplayName("Tên Danh Mục")]
            public int MaDanhMuc { get; set; }
            [Required(ErrorMessage = "Tên loại sản phẩm không được để trống!")]
            [DisplayName("Tên Loại Sản Phẩm")]
            public string TenLoaiSP { get; set; }
            public string Icon { get; set; }
            public string BiDanh { get; set; }
        }
    }
}