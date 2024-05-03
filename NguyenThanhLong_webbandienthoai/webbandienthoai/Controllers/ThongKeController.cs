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
                ViewBag.TongDonDatHang = DonDatHang();
                ViewBag.TKDoanhThuThang = ThongKeDoanhThuTheoThang();
                ViewBag.TKDoanhThuTuan = ThongKeDoanhThuTheoTuan();
                ViewBag.ThongKeSP = ThongKeSP();
                ViewBag.ThongKePhieuNhap = ThongKePhieuNhap();
                ViewBag.TongThanhVien = ThanhVien();
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "Login");
            }
        }
        protected decimal ThongKeDoanhThuTheoThang()
        {
            var thanghientai = DateTime.Now.Month;
            var namhientai = DateTime.Now.Year;
            var listRevenue = db.DonDatHangs.Where(row => row.TinhTrang == "Đã giao hàng" && row.NgayGiao.Value.Month==thanghientai && row.NgayGiao.Value.Year==namhientai);

            if (!listRevenue.Any())
            {
                return 0;
            }
            decimal total = 0;
            foreach (var item in listRevenue)
            {
                total += item.ChiTietDonDatHangs.Sum(row => row.SoLuong * row.DonGia);
            }
            return total;
        }
        private void GetWeekRange(DateTime date, out DateTime startOfWeek, out DateTime endOfWeek)
        {
            // Xác định ngày đầu tuần (Thứ Hai)
            DayOfWeek firstDay = DayOfWeek.Monday;
            int offset = date.DayOfWeek - firstDay;
            if (offset < 0)
                offset += 7;

            startOfWeek = date.AddDays(-offset).Date;
            endOfWeek = startOfWeek.AddDays(6).Date;
        }

        protected decimal ThongKeDoanhThuTheoTuan()
        {
            var toDay = DateTime.Now;
            GetWeekRange(toDay,out DateTime startOfWeek, out DateTime endOfWeek);
            var listRevenue = db.DonDatHangs.Where(row => row.TinhTrang == "Đã giao hàng" && row.NgayGiao>=startOfWeek && row.NgayGiao<=endOfWeek);

            if (!listRevenue.Any())
            {
                return 0;
            }
            decimal total = 0;
            foreach (var item in listRevenue)
            {
                total += item.ChiTietDonDatHangs.Sum(row => row.SoLuong * row.DonGia);
            }
            return total;
        }
        public int ThongKeSP()
        {
            return db.SanPhams.Where(n=>n.DaXoa==false).Count();
        }
        public int ThongKePhieuNhap()
        {
            return db.PhieuNhaps.Count();
        }
        protected int DonDatHang()
        {
            var lstDDH = db.DonDatHangs.Where(n=>n.TinhTrang=="Chưa phê duyệt").Count();
            return lstDDH;
        } 
        protected int ThanhVien()
        {
            var lstThanhVien = db.ThanhViens.Count();
            return lstThanhVien;
        }
        public ActionResult PhieuNhapHang()
        {
            var pn = db.PhieuNhaps;
            return View(pn);
        }
        public ActionResult ChiTietPN(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var pn = db.PhieuNhaps.Single(n=>n.MaPN==id);
            var lstpn = db.ChiTietPhieuNhaps.Where(n=>n.MaPN==pn.MaPN);
            ViewBag.lstpn = lstpn;
            return View(pn);
        }
        public ActionResult XoaPN(int? id)
        {
            if (id == null)
            {
                Response.StatusCode=404;
                return null;
            }
            var pn = db.PhieuNhaps.Single(n => n.MaPN == id);
            var lstpn = db.ChiTietPhieuNhaps.Where(n => n.MaPN == pn.MaPN);
            ViewBag.lstpn = lstpn;
            return View(pn);
        }
        [HttpPost]
        public ActionResult XoaPN(int MaPN)
        {
            var pn = db.PhieuNhaps.Single(n => n.MaPN == MaPN);
            var lstpn = db.ChiTietPhieuNhaps.Where(n => n.MaPN == pn.MaPN);
            db.ChiTietPhieuNhaps.RemoveRange(lstpn);
            db.PhieuNhaps.Remove(pn);
            db.SaveChanges();
            TempData["xoapn"] = "Xóa phiếu nhập thành công";
            return RedirectToAction("PhieuNhapHang","ThongKe");
        }

    }
}