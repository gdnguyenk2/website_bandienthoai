using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            KhachHang kh = db.KhachHangs.SingleOrDefault(n=>n.MaTV==tv.MaTV);
            var iddonhang = db.DonDatHangs.SingleOrDefault(n=>n.MaKH== kh.MaKH);
            var listctdonhang = db.ChiTietDonDatHangs.Where(n=>n.MaDDH==iddonhang.MaDDH);
            return View(listctdonhang);
        }
    }
}