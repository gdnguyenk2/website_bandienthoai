using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using webbandienthoai.Models;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
namespace webbandienthoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyDonHangController : Controller
    {
        // GET: QuanLyDonHang
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        public ActionResult PheDuyet()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            if (tv != null)
            {
                //lấy danh sách các đơn hàng chưa đc duyệt
                var lstGiao = db.DonDatHangs.Where(n => n.TinhTrang == "Chưa phê duyệt").OrderByDescending(n => n.NgayDatHang);
                return View(lstGiao);
            }
            else
            {
                return RedirectToAction("DangNhap", "Login");
            }
        }
        public ActionResult ChuaGiao()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            if (tv != null)
            {
                //lấy danh sách các đơn hàng chưa đc duyệt
                var lstGiao = db.DonDatHangs.Where(n => n.TinhTrang == "Đã phê duyệt").OrderByDescending(n => n.NgayDatHang);
                return View(lstGiao);
            }
            else
            {
                return RedirectToAction("DangNhap", "Login");
            }
        }
        public ActionResult DaGiaoDaThanhToan()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            if (tv != null)
            {
                //lấy danh sách các đơn hàng chưa đc duyệt
                var lstGiao = db.DonDatHangs.Where(n => n.TinhTrang == "Đã giao hàng" && n.DaThanhToan == "Đã thanh toán").OrderByDescending(n => n.NgayGiao);
                return View(lstGiao);
            }
            else
            {
                return RedirectToAction("DangNhap", "Login");
            }
        }
        public ActionResult HuyHang()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            if (tv != null)
            {
                //lấy danh sách các đơn hàng chưa đc duyệt
                var lstGiao = db.DonDatHangs.Where(n => n.TinhTrang == "Đã hủy").OrderByDescending(n => n.NgayDatHang);
                return View(lstGiao);
            }
            else
            {
                return RedirectToAction("DangNhap", "Login");
            }
        }
        public ActionResult YeuCauHuy()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            if (tv != null)
            {
                var lstGiao = db.DonDatHangs.Where(n => n.TinhTrang == "Hủy đơn hàng").OrderByDescending(n => n.NgayDatHang);
                return View(lstGiao);
            }
            else
            {
                return RedirectToAction("DangNhap", "Login");
            }
        }
        public ActionResult DuyetDonHang(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;  
                return null;
            }
            DonDatHang ddh = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == id);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            //Lấy chi tiết đơn hàng hiển thị cho người dùng thấy
            var ctdh = db.ChiTietDonDatHangs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.ctdh = ctdh;
            return View(ddh);
        }
        [HttpPost]
        public ActionResult DuyetDonHang(int MaDDH)
        {
            DonDatHang ddhup = db.DonDatHangs.Single(n=>n.MaDDH== MaDDH);
            ddhup.TinhTrang = "Đã phê duyệt";
            db.SaveChanges();

            //Lấy danh sách chi tiết đơn hàng đẻ hiển thị cho người dùng thấy
            var ctdh = db.ChiTietDonDatHangs.Where(n => n.MaDDH == MaDDH);
            ViewBag.ctdh = ctdh;


            return RedirectToAction("ChuaGiao","QuanLyDonHang");
        }
        public ActionResult HoanThanhDon(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DonDatHang ddh = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == id);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            //Lấy chi tiết đơn hàng hiển thị cho người dùng thấy
            var ctdh = db.ChiTietDonDatHangs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.ctdh = ctdh;
            return View(ddh);
        }
        [HttpPost]
        public ActionResult HoanThanhDon(int MaDDH)
        {
            //Lấy danh sách chi tiết đơn hàng đẻ hiển thị cho người dùng thấy
            var ctdh = db.ChiTietDonDatHangs.Where(n => n.MaDDH == MaDDH);
            ViewBag.ctdh = ctdh;
            DonDatHang ddhup = db.DonDatHangs.Single(n => n.MaDDH == MaDDH);
            foreach (var item in ctdh)
            {
                SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == item.MaSP);
                sp.SoLanMua += item.SoLuong;
            }
            ddhup.DaThanhToan = "Đã thanh toán";
            ddhup.TinhTrang = "Đã giao hàng";
            ddhup.NgayGiao = DateTime.Now;
            db.SaveChanges();


            return RedirectToAction("DaGiaoDaThanhToan", "QuanLyDonHang");
        }
        public ActionResult InDonHang(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DonDatHang ddh = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == id);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            //Lấy chi tiết đơn hàng hiển thị cho người dùng thấy
            var ctdh = db.ChiTietDonDatHangs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.ctdh = ctdh;
            return View(ddh);
        }
        public ActionResult HuyDonHang(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DonDatHang ddh = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == id);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            //Lấy chi tiết đơn hàng hiển thị cho người dùng thấy
            var ctdh = db.ChiTietDonDatHangs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.ctdh = ctdh;
            return View(ddh);
        }
        [HttpPost]
        public ActionResult HuyDonHang(int MaDDH)
        {
            DonDatHang ddhup = db.DonDatHangs.Single(n => n.MaDDH == MaDDH);
            ddhup.TinhTrang = "Đã hủy";
            db.SaveChanges();

            //Lấy danh sách chi tiết đơn hàng đẻ hiển thị cho người dùng thấy
            var ctdh = db.ChiTietDonDatHangs.Where(n => n.MaDDH == MaDDH);
            ViewBag.ctdh = ctdh;


            return RedirectToAction("HuyHang", "QuanLyDonHang");
        }

    }
}