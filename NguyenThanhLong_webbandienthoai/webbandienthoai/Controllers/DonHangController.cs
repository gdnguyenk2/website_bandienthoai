using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using webbandienthoai.Models;

namespace webbandienthoai.Controllers
{
    public class DonHangController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        // GET: DonHang
        public ActionResult XemDonHang()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            List<KhachHang> kh = db.KhachHangs.Where(n=>n.MaTV==tv.MaTV).ToList();
            var lstiddonhang = new List<DonDatHang>();
            foreach (var item in kh)
            {
                var iddonhang = db.DonDatHangs.SingleOrDefault(n => n.MaKH == item.MaKH);
                if (iddonhang != null)
                {
                    lstiddonhang.Add(iddonhang);
                } 
            }

            return View(lstiddonhang.Where(n => n.TinhTrang == "Chưa phê duyệt" || n.TinhTrang=="Hủy đơn hàng").OrderByDescending(n=>n.NgayDatHang));
        }
        [HttpPost]
        public ActionResult HuyDonHang(int MaDDH)
        {
            var ddh = db.DonDatHangs.SingleOrDefault(n=>n.MaDDH==MaDDH);
            if (ddh != null)
            {
                ddh.TinhTrang = "Hủy đơn hàng";
                db.SaveChanges();
                return Json(new {message="Đã gửi yêu cầu hủy đơn hàng"});
            }
            else
            {
                return Json(new { message = "Lỗi khi gửi đơn hàng" });
            }
        }
        public ActionResult DangGiao()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            List<KhachHang> kh = db.KhachHangs.Where(n => n.MaTV == tv.MaTV).ToList();
            var lstiddonhang = new List<DonDatHang>();
            foreach (var item in kh)
            {
                var iddonhang = db.DonDatHangs.SingleOrDefault(n => n.MaKH == item.MaKH);
                if (iddonhang != null)
                {
                    lstiddonhang.Add(iddonhang);
                }
            }

            return View(lstiddonhang.Where(n=>n.TinhTrang=="Đã phê duyệt").OrderByDescending(n => n.NgayDatHang));
        }
        public ActionResult DaHoanThanh()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            List<KhachHang> kh = db.KhachHangs.Where(n => n.MaTV == tv.MaTV).ToList();
            var lstiddonhang = new List<DonDatHang>();
            foreach (var item in kh)
            {
                var iddonhang = db.DonDatHangs.SingleOrDefault(n => n.MaKH == item.MaKH);
                if (iddonhang != null)
                {
                    lstiddonhang.Add(iddonhang);
                }
            }

            return View(lstiddonhang.Where(n => n.TinhTrang == "Đã giao hàng").OrderByDescending(n => n.NgayDatHang));
        }
        public ActionResult DaHuy()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            List<KhachHang> kh = db.KhachHangs.Where(n => n.MaTV == tv.MaTV).ToList();
            var lstiddonhang = new List<DonDatHang>();
            foreach (var item in kh)
            {
                var iddonhang = db.DonDatHangs.SingleOrDefault(n => n.MaKH == item.MaKH);
                if (iddonhang != null)
                {
                    lstiddonhang.Add(iddonhang);
                }
            }

            return View(lstiddonhang.Where(n => n.TinhTrang == "Đã hủy").OrderByDescending(n => n.NgayDatHang));
        }
    }
}