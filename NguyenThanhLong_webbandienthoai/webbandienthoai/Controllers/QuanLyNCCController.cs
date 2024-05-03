using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbandienthoai.Models;

namespace webbandienthoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyNCCController : Controller
    {
        // GET: QuanLyNCC
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        public ActionResult Index()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            if (tv != null)
            {
                return View(db.NhaCungCaps.OrderByDescending(n => n.MaNCC));
            }
            else
            {
                return RedirectToAction("DangNhap", "Login");
            }
        }
        public ActionResult ThemNhaCungCap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNhaCungCap(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                db.NhaCungCaps.Add(ncc);
                db.SaveChanges();
                TempData["themncc"] = "Thêm nhà cung cấp thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(ncc);
            }

        }
    }
}