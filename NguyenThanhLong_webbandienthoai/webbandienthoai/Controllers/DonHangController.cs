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
            List<KhachHang> kh = db.KhachHangs.Where(n=>n.MaTV==tv.MaTV).ToList();
            var listctdonhang = new List<ChiTietDonDatHang>();
            foreach(var item in kh)
            {
                var iddonhang = db.DonDatHangs.SingleOrDefault(n => n.MaKH == item.MaKH);
                var ctdh = db.ChiTietDonDatHangs.Where(n => n.MaDDH == iddonhang.MaDDH);
                foreach(var item2 in ctdh)
                {
                    listctdonhang.Add(item2);
                }
            }
            
            return View(listctdonhang.OrderByDescending(n=>n.DonDatHang.NgayDatHang));
        }
    }
}