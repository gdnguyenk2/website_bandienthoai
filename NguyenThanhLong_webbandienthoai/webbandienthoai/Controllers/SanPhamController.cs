using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webbandienthoai.Models;
using static System.Net.WebRequestMethods;

namespace webbandienthoai.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();

        [ChildActionOnly]
        //tạo partial để hiển thị style
        public ActionResult SanPhamPatial()
        {
            //sp moi
            var lstMoi = db.SanPhams.Where(m => m.Moi == true && m.DaXoa == false).ToList();
            ViewBag.Moi = lstMoi;
            //sp bán chạy
            return PartialView(lstMoi);
        }
        [ChildActionOnly]
        public ActionResult SanPhamPatial2()
        {
            //sp moi
            var lstBanChay = db.SanPhams.Where(m => m.BanChay == true && m.DaXoa == false).ToList();
            ViewBag.BanChay = lstBanChay;
            //sp bán chạy
            return PartialView(lstBanChay);
        }

        public ActionResult ChiTietSanPham(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SanPham product = db.SanPhams.Where(row => row.MaSP == id && row.DaXoa == false).SingleOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }
            List<int> corlorIds = db.SanPham_Mau.Where(row => row.MaSP == id).Select(row => row.MaMau).ToList();
            List<Mau> productColors = db.Maus.Where(row => corlorIds.Contains(row.MaMau)).ToList();
            ViewBag.listMau = productColors;

            LoaiSanPham loaiid= db.LoaiSanPhams.SingleOrDefault(m=>m.MaLoaiSP ==product.MaLoaiSP);
            List<SanPham> sp_tuongtu = db.SanPhams.Where(m => m.MaLoaiSP == loaiid.MaLoaiSP).ToList();
            ViewBag.sptuongtu= sp_tuongtu;
            ViewBag.masp = id;
            return View(product);
        }
        public ActionResult SanPhamAll()
        {
            return View();
        }
        public ActionResult hienThiLoaiSP(int? id)
        {
            NhaSanXuat NSXSP = db.NhaSanXuats.SingleOrDefault(m => m.MaNSX == id);
            List<SanPham> sp = db.SanPhams.Where(m => m.MaNSX == NSXSP.MaNSX).ToList(); 
            return View(sp);
        }
    }
}