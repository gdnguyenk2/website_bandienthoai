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
            //lấy danh sách các đơn hàng chưa đc duyệt
            var lstGiao = db.DonDatHangs.Where(n => n.TinhTrang == "Chưa phê duyệt" && n.DaThanhToan == false).OrderBy(n => n.NgayGiao);
            return View(lstGiao);
        }
        public ActionResult ChuaGiao()
        {
            //lấy danh sách các đơn hàng chưa đc duyệt
            var lstGiao = db.DonDatHangs.Where(n => n.TinhTrang == "Đang giao hàng" && n.DaThanhToan==false).OrderBy(n => n.NgayGiao);
            return View(lstGiao);
        }
        public ActionResult DaGiaoDaThanhToan()
        {
            //lấy danh sách các đơn hàng chưa đc duyệt
            var lstGiao = db.DonDatHangs.Where(n => n.TinhTrang == "Đã giao hàng"&&n.DaThanhToan==true).OrderBy(n => n.NgayGiao);
            return View(lstGiao);
        }
        public ActionResult DuyetDonHang(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DonDatHang ddh = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == id);
            ViewBag.DaThanhToan = ddh.DaThanhToan == true ? "Đã thanh toán" : "Chưa thanh toán";
            ViewBag.TinhTrang = ddh.TinhTrang.ToString();
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
            ddhup.DaThanhToan = false;
            ddhup.TinhTrang = "Đang giao hàng";
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
            ViewBag.DaThanhToan = ddh.DaThanhToan == true ? "Đã thanh toán" : "Chưa thanh toán";
            ViewBag.TinhTrang = ddh.TinhTrang.ToString();
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
            DonDatHang ddhup = db.DonDatHangs.Single(n => n.MaDDH == MaDDH);
            ddhup.DaThanhToan = true;
            ddhup.TinhTrang = "Đã giao hàng";
            ddhup.NgayGiao = DateTime.Now;
            db.SaveChanges();

            //Lấy danh sách chi tiết đơn hàng đẻ hiển thị cho người dùng thấy
            var ctdh = db.ChiTietDonDatHangs.Where(n => n.MaDDH == MaDDH);
            ViewBag.ctdh = ctdh;


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
            ViewBag.DaThanhToan = ddh.DaThanhToan == true ? "Đã thanh toán" : "Chưa thanh toán";
            ViewBag.TinhTrang = ddh.TinhTrang.ToString();
            if (ddh == null)
            {
                return HttpNotFound();
            }
            //Lấy chi tiết đơn hàng hiển thị cho người dùng thấy
            var ctdh = db.ChiTietDonDatHangs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.ctdh = ctdh;
            return View(ddh);
        }

    }
}