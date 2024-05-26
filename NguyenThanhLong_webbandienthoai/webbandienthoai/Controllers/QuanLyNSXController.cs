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
    public class QuanLyNSXController : Controller
    {
        // GET: QuanLyNSX
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        public ActionResult Index()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            if (tv != null)
            {
                return View(db.NhaSanXuats.OrderByDescending(n => n.MaNSX));
            }
            else
            {
                return RedirectToAction("DangNhap", "Login");
            }
        }
        public ActionResult ThemNhaSanXuat()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNhaSanXuat(NhaSanXuat nsx, HttpPostedFileBase Logo)
        {
            if (ModelState.IsValid)
            {
                //Kiểm tra hình có tồn tại trong csdl ko
                if (Logo != null)
                {
                    if (Logo.ContentLength > 0)
                    {
                        //Lấy tên hình ảnh
                        var fileName = Path.GetFileName(Logo.FileName);
                        //Lấy hình ảnh chuyển vào thư mục hình ảnh
                        var path = Path.Combine(Server.MapPath("~/Content/Images/ProductLogos"), fileName);
                        //Nếu thư mục có hình ảnh rồi thì thông báo
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.upload = "Hình đã tồn tại";
                            return View(nsx);
                        }
                        else
                        {
                            //Lấy hình ảnh đưa vào thư mục
                            Logo.SaveAs(path);
                            nsx.Logo = fileName;
                        }
                    }
                }
                else
                {
                    return View(nsx);
                }
                db.NhaSanXuats.Add(nsx);
                db.SaveChanges();
                TempData["themnsx"] = "Thêm nhà sản xuất thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(nsx);
            }

        }
    }
}