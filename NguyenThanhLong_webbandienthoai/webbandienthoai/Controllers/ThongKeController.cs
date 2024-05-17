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
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            if (tv != null)
            {
                ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"];//Lấy số lượng người từ application
                ViewBag.SoNguoiTrucTuyen = HttpContext.Application["SoNguoiTrucTuyen"];
                ViewBag.TongDoanhThu = ThongKeDoanhThu();
                ViewBag.TongDonDatHang = DonDatHang();
                ViewBag.TongThanhVien = ThanhVien();
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "Login");
            }
        }
        protected decimal ThongKeDoanhThu()
        {

            var listRevenue = db.DonDatHangs.Where(row => row.TinhTrang == "Đã giao hàng");

            decimal total = decimal.Zero;
            foreach (var item in listRevenue)
            {
                total += decimal.Parse(item.ChiTietDonDatHangs.Sum(row => row.SoLuong * row.DonGia).ToString());
            }
            return total;
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