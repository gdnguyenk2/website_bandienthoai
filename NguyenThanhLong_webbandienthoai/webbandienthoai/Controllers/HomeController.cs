using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbandienthoai.Models;

namespace webbandienthoai.Controllers
{
    public class HomeController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Slider()
        {
            var listQuangCao = db.QuangCaos;
            return PartialView(listQuangCao);
        }
        public ActionResult MenuPar()
        {
            var lstsp = db.SanPhams.ToList();
            return PartialView(lstsp);
        }
        [ChildActionOnly]
        public ActionResult Herder_top()
        {
            if (Request.Cookies["RememberMeCookie"] != null && !string.IsNullOrEmpty(Request.Cookies["RememberMeCookie"]["TaiKhoan"]))
            {
                string username = Request.Cookies["RememberMeCookie"]["TaiKhoan"].ToString();
                ThanhVien member = db.ThanhViens.Where(row => row.TaiKhoan == username).SingleOrDefault();

                if (member != null)
                {
                    Session["member"] = member;
                }
            }

            return PartialView();
        }
        public ActionResult Quyen()
        {
            return View();
        }
        public ActionResult footer()
        {
            var lstsp = db.SanPhams.ToList();
            return PartialView(lstsp);
        }
        public ActionResult ThongTinCaNhan()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;

            if (tv == null)
            {
                return RedirectToAction("DangNhap", "Login");
            }
            return View(tv);
        }
        [HttpPost]
        public ActionResult CapNhatThongTin(ThanhVien member, HttpPostedFileBase avartar)
        {
            if (member == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ThanhVien memberUpdate = db.ThanhViens.Where(row => row.MaTV == member.MaTV).SingleOrDefault();

            if (memberUpdate == null)
            {
                return HttpNotFound();
            }

            memberUpdate.HoTen = member.HoTen;
            memberUpdate.Email = member.Email;
            memberUpdate.SoDienThoai = member.SoDienThoai;
            memberUpdate.DiaChi = member.DiaChi;
            if (avartar != null)
            {
                if (avartar.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(avartar.FileName);

                    var path = Path.Combine(Server.MapPath("~/Content/Images/Members"), fileName);

                    avartar.SaveAs(path);

                    memberUpdate.HinhDaiDien = fileName;
                }
            }
            else
            {
                memberUpdate.HinhDaiDien = memberUpdate.HinhDaiDien;
            }

            Session["TaiKhoans"] = memberUpdate;

            db.SaveChanges();

            return Json(new { message = "Thay đổi thông tin thành công" });
        }

        [HttpGet]
        public ActionResult ThayMatKhau()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;

            if (tv == null)
            {
                return RedirectToAction("DangNhap", "Login");
            }

            return View(tv);
        }

        [HttpPost]
        public ActionResult ThayMatKhau(ThanhVien tv, string oldPassword, string newPassword, string confirmPassword)
        {
            if (tv == null)
            {
                return HttpNotFound();
            }

            if (tv.MatKhau != oldPassword)
            {
                return Json(new { message = "Mật khẩu hiện tại không đúng." });

            }

            if (oldPassword == newPassword)
            {
                return Json(new { message = "Mật khẩu mới không được trùng với mật khẩu cũ." });
            }

            if (newPassword != confirmPassword)
            {
                return Json(new { message = "Xác nhận mật khẩu không trùng khớp" });
            }

            ThanhVien tvUpdate = db.ThanhViens.Where(n => n.MaTV == tv.MaTV).SingleOrDefault();

            if (tvUpdate == null)
            {
                return HttpNotFound();
            }

            tvUpdate.MatKhau = newPassword;

            db.SaveChanges();

            return Json(new { message = "Đổi mật khẩu thành công." });
        }
    }
}