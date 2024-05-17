using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webbandienthoai.Models
{
    public class GioHangItem
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }

        public GioHangItem(int MaSP)
        {
            using (WebBanDienThoaiEntities db = new WebBanDienThoaiEntities())
            {
                this.MaSP = MaSP;
                SanPham sp = db.SanPhams.Single(n => n.MaSP == MaSP);
                this.TenSP = sp.TenSP;
                this.HinhAnh = sp.HinhAnh;
                this.SoLuong = 1;
                this.DonGia = sp.DonGia.Value;
                this.ThanhTien = this.DonGia * SoLuong;
            }
        }
        public GioHangItem(int iMaSP, int sl) 
        {
            using (WebBanDienThoaiEntities db = new WebBanDienThoaiEntities())
            {
                this.MaSP = iMaSP;
                SanPham sp = db.SanPhams.Single(n => n.MaSP == iMaSP);
                this.TenSP = sp.TenSP;
                this.HinhAnh = sp.HinhAnh;
                this.SoLuong = sl;
                if (sp.MaKhuyenMai == null || sp.MaKhuyenMai==0)
                {
                    this.DonGia = sp.DonGia.Value;
                }
                else
                {
                    this.DonGia = sp.DonGia.Value - (sp.DonGia.Value * sp.KhuyenMai.PhanTramGiamGia / 100);
                }
                this.ThanhTien = DonGia * SoLuong;
            }
        }
        public GioHangItem() { }
    }
}