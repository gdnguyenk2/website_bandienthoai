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
        public int MaMau { get; set; }
        public string TenMau { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }

        public GioHangItem(int MaSP, decimal DonGia, int SoLuong, int MaMau)
        {
            using (WebBanDienThoaiEntities db = new WebBanDienThoaiEntities())
            {
                this.MaSP = MaSP;
                this.MaMau = MaMau;

                SanPham product = db.SanPhams.Where(row => row.MaSP == MaSP).SingleOrDefault();

                Mau color = db.Maus.Where(row => row.MaMau == MaMau).SingleOrDefault();

                this.TenMau = color.TenMau;
                this.TenSP = product.TenSP + " - " + this.TenMau;

                this.SoLuong = SoLuong;
                this.DonGia = DonGia;
                this.ThanhTien = this.SoLuong * this.DonGia;
                this.HinhAnh = product.HinhAnh;
            }
        }

        public GioHangItem(int MaSP, decimal DonGia, int SoLuong)
        {
            using (WebBanDienThoaiEntities db = new WebBanDienThoaiEntities())
            {
                this.MaSP = MaSP;

                SanPham product = db.SanPhams.Where(row => row.MaSP == MaSP).SingleOrDefault();

                this.TenSP = product.TenSP;

                this.SoLuong = SoLuong;
                this.DonGia = DonGia;
                this.ThanhTien = this.SoLuong * this.DonGia;
                this.HinhAnh = product.HinhAnh;
            }
        }

        public GioHangItem(string TenSP, int MaSP, decimal DonGia, int SoLuong)
        {
            using (WebBanDienThoaiEntities db = new WebBanDienThoaiEntities())
            {
                this.MaSP = MaSP;

                SanPham product = db.SanPhams.Where(row => row.MaSP == MaSP).SingleOrDefault();

                this.TenSP = TenSP;

                this.SoLuong = SoLuong;
                this.DonGia = DonGia;
                this.ThanhTien = this.SoLuong * this.DonGia;
                this.HinhAnh = product.HinhAnh;
            }
        }
    }
}