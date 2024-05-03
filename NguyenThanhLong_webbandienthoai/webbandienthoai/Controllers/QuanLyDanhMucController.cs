using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbandienthoai.Models;
namespace webbandienthoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyDanhMucController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        // GET: QuanLyDanhMuc
        public ActionResult Index()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            if (tv != null)
            {
                var listloaisp = db.LoaiSanPhams.OrderByDescending(n => n.MaLoaiSP);
                return View(listloaisp);
            }
            else
            {
                return RedirectToAction("DangNhap", "Login");
            }
        }
        public ActionResult ThemLoaiSP()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs.OrderBy(n => n.MaDanhMuc), "MaDanhMuc", "TenDanhMuc");
            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiSP(LoaiSanPham loaisp)
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs.OrderBy(n => n.MaDanhMuc), "MaDanhMuc", "TenDanhMuc");
            if (ModelState.IsValid)
            {
                db.LoaiSanPhams.Add(loaisp);
                db.SaveChanges();
                TempData["themloaisp"] = "Thêm loại sản phẩm thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(loaisp);
            }
        }
    }
}