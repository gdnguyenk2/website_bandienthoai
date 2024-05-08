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
        public ActionResult DangKy(ThanhVien tv)
        {
            if (this.IsCaptchaValid("Mã captcha không đúng."))
            {
                if (ModelState.IsValid)
                {
                    db.ThanhViens.Add(tv);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }
        public JsonResult KTTaiKhoan(string username)
        {
            bool kttaikhoan = db.ThanhViens.Any(row => row.TaiKhoan == username);
            return Json(kttaikhoan, JsonRequestBehavior.AllowGet);
        }

    }
}