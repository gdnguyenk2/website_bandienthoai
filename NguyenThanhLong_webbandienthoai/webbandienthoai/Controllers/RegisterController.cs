using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using webbandienthoai.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using Microsoft.Ajax.Utilities;
using System.Web.Helpers;
namespace webbandienthoai.Controllers
{
    public class RegisterController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(ThanhVien tv)
        {
            if (this.IsCaptchaValid("Mã captcha không đúng."))
            {
                if (ModelState.IsValid)
                {
                    var kttaikhoan = db.ThanhViens.Any(row => row.TaiKhoan == tv.TaiKhoan);
                    if (kttaikhoan)
                    {
                        return View();
                    }
                    var ktemail = db.ThanhViens.Any(row => row.Email == tv.Email);
                    if (ktemail)
                    {
                        return View();
                    }
                    tv.HinhDaiDien = "default.png";
                    db.ThanhViens.Add(tv);
                    db.SaveChanges();
                    TempData["dktc"] = "Đăng ký thành công";
                    return RedirectToAction("DangNhap", "Login");
                }
            }

            return View();
        }
        public JsonResult KTTaiKhoan(string username)
        {
            bool kttaikhoan = db.ThanhViens.Any(row => row.TaiKhoan == username);
            return Json(kttaikhoan, JsonRequestBehavior.AllowGet);
        }
        public JsonResult KTEmail(string email)
        {
            bool ktemail = db.ThanhViens.Any(row => row.Email == email);
            return Json(ktemail, JsonRequestBehavior.AllowGet);
        }
    }
}