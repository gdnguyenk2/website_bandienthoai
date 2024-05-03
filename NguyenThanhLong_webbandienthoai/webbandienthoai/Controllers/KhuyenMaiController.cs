using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbandienthoai.Models;
namespace webbandienthoai.Controllers
{
    [Authorize(Roles ="Admin")]
    public class KhuyenMaiController : Controller
    {
        // GET: KhuyenMai
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        public ActionResult Index()
        {
            var lstKhuyenMai = db.KhuyenMais.OrderBy(n=>n.PhanTramGiamGia);
            return View(lstKhuyenMai);
        }
        public ActionResult ThemKM()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemKM(KhuyenMai km)
        {
            if (ModelState.IsValid) // Kiểm tra dữ liệu đã được nhập đúng chưa
            {
                db.KhuyenMais.Add(km);
                db.SaveChanges();
                return RedirectToAction("Index"); // Hoặc chuyển hướng đến một trang khác
            }
            else
            {
                return View(km); // Trả về lại View với dữ liệu đã nhập
            }
        }
        public ActionResult Sua(int? MaKhuyenMai)
        {
            if (MaKhuyenMai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            KhuyenMai km = db.KhuyenMais.SingleOrDefault(n => n.MaKhuyenMai == MaKhuyenMai);
            if (km == null)
            {
                return HttpNotFound();
            }
            return View(km);
        }
        [HttpPost]
        public ActionResult Sua(KhuyenMai km)
        {
            if (ModelState.IsValid) // Kiểm tra dữ liệu đã được nhập đúng chưa
            {
                db.Entry(km).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); // Hoặc chuyển hướng đến một trang khác
            }
            else
            {
                return View(km); // Trả về lại View với dữ liệu đã nhập
            }
        }
        public ActionResult Xoa(int? MaKhuyenMai)
        {
            if (MaKhuyenMai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            KhuyenMai km = db.KhuyenMais.SingleOrDefault(n => n.MaKhuyenMai == MaKhuyenMai);
            if (km == null)
            {
                return HttpNotFound();
            }
            return View(km);
        }
        [HttpPost]
        public ActionResult Xoa(int MaKhuyenMai)
        {
            KhuyenMai km = db.KhuyenMais.SingleOrDefault(n => n.MaKhuyenMai == MaKhuyenMai);
            if (km == null)
            {
                return HttpNotFound();
            }
            db.KhuyenMais.Remove(km);
            db.SaveChanges();
            return RedirectToAction("Index","KhuyenMai");
        }

    }
}