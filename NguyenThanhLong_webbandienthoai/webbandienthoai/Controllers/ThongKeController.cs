using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbandienthoai.Models;
namespace webbandienthoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ThongKeController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        // GET: ThongKe
        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"];//Lấy số lượng người từ application
            ViewBag.SoNguoiTrucTuyen = HttpContext.Application["SoNguoiTrucTuyen"];
            ViewBag.TongDoanhThu = ThongKeDoanhThu();
            ViewBag.TongDonDatHang = DonDatHang();
            ViewBag.TongThanhVien = ThanhVien();
            return View();
        }
        protected decimal ThongKeDoanhThu()
        {
            decimal TongDoanhThu = db.ChiTietDonDatHangs.Sum(n=>n.SoLuong * n.DonGia);
            return TongDoanhThu;
        }
        protected int DonDatHang()
        {
            var lstDDH = db.DonDatHangs.Count();
            return lstDDH;
        } 
        protected int ThanhVien()
        {
            var lstThanhVien = db.ThanhViens.Count();
            return lstThanhVien;
        }
    }
}