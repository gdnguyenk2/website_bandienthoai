using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webbandienthoai.Models
{
    [MetadataTypeAttribute(typeof(SanPhamMetadata))]
    public partial class SanPham
    { 
        internal sealed class SanPhamMetadata
        {
            
            public int MaSP { get; set; }
            
            public int MaNCC { get; set; }
            public int MaNSX { get; set; }
            public int MaLoaiSP { get; set; }
            public Nullable<int> MaKhuyenMai { get; set; }
            [Required(ErrorMessage ="Tên Sản Phẩm không được bỏ trống!")]
            [DisplayName("Tên Sản Phẩm")]
            public string TenSP { get; set; }
            [Required(ErrorMessage ="Đơn giá không được bỏ trống!")]
            [DisplayName("Đơn giá")]
            public Nullable<decimal> DonGia { get; set; }
            [Required(ErrorMessage ="Ngày cập nhật không được bỏ trống!")]
            [DisplayName("Ngày cập nhật")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
            [Required(ErrorMessage ="Mô tả không được bỏ trống!")]
            [DisplayName("Mô tả")]
            public string MoTa { get; set; }
            [Required(ErrorMessage ="Cấu hình không được bỏ trống!")]
            [DisplayName("Cấu Hình")]
            public string CauHinh { get; set; }
            [Required(ErrorMessage ="Vui lòng chọn ảnh")]
            [DisplayName("Hình Ảnh")]
            public string HinhAnh { get; set; }
            [Required(ErrorMessage = "Vui lòng chọn ảnh")]
            [DisplayName("Hình Ảnh 2")]
            public string HinhAnh2 { get; set; }
            [Required(ErrorMessage = "Vui lòng chọn ảnh")]
            [DisplayName("Hình Ảnh 3")]
            public string HinhAnh3 { get; set; }
            [Required(ErrorMessage = "Vui lòng chọn ảnh")]
            [DisplayName("Hình Ảnh 4")]
            public string HinhAnh4 { get; set; }
            [Required(ErrorMessage = "Số lượng tồn không được để trống!")]
            [DisplayName("Số lượng Tồn")]
            public int SoLuongTon { get; set; }
            [DisplayName("Lượt Xem")]
            public Nullable<int> LuotXem { get; set; }
            [DisplayName("Lượt Bình Chọn")]
            public Nullable<int> LuotBinhChon { get; set; }
            [DisplayName("Lượt Bình Luận")]
            public Nullable<int> LuotBinhLuan { get; set; }
            [DisplayName("Số Lần Mua")]
            public Nullable<int> SoLanMua { get; set; }
            [DisplayName("Mới")]
            public bool Moi { get; set; }
            [DisplayName("Bán Chạy")]
            public bool BanChay { get; set; }
            [DisplayName("Đã Xóa")]
            public bool DaXoa { get; set; }
        }
    }
}