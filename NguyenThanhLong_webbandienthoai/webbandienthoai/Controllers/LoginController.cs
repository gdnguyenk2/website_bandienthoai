using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using webbandienthoai.Models;

namespace webbandienthoai.Controllers
{
    public class LoginController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(ThanhVien truycap, bool RememberMe = false)
        {
            ThanhVien tv = db.ThanhViens.Where(row => row.TaiKhoan == truycap.TaiKhoan && row.MatKhau == truycap.MatKhau).SingleOrDefault();
            if (tv != null)
            {
                Session["TaiKhoans"] = tv;

                if (RememberMe)
                {
                    HttpCookie cookie = new HttpCookie("RememberMeCookie");
                    cookie.Values["TaiKhoan"] = tv.TaiKhoan;
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    if (Request.Cookies["RememberMeCookie"] != null)
                    {
                        Response.Cookies.Remove("RememberMeCookie");
                        Response.Cookies["RememberMeCookie"].Expires = DateTime.Now.AddYears(-1);
                    }
                }

                IEnumerable<LoaiThanhVien_Quyen> listQuyen = db.LoaiThanhVien_Quyen.Where(row => row.MaLoaiTV == tv.MaLoaiTV);
                string CacQuyen = "";
                foreach (var item in listQuyen)
                {
                    CacQuyen += item.Quyen.MaQuyen + ",";
                }
                CacQuyen = CacQuyen.Substring(0, CacQuyen.Length - 1);
                GrantPermissions(tv.TaiKhoan, CacQuyen);

                if (CacQuyen.Contains("Admin"))
                {
                    string script = "<script>window.location.href = '" + Url.Action("Index", "ThongKe") + "';</script>";
                    return Content(script);
                }
                else
                {
                    string script = "<script>window.location.href = '" + Url.Action("Index", "Home") + "';</script>";
                    return Content(script);
                }
            }
            ViewBag.Loi = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
        //Phân quyền
        public void GrantPermissions(string tk, string quyen)
        {
            FormsAuthentication.Initialize();

            var ticket = new FormsAuthenticationTicket(1, tk, DateTime.Now, DateTime.Now.AddHours(3), false, quyen, FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

            Response.Cookies.Add(cookie);
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoans"] = null;
            FormsAuthentication.SignOut();
            if (Request.Cookies["RememberMeCookie"] != null)
            {
                Response.Cookies.Remove("RememberMeCookie");
                Response.Cookies["RememberMeCookie"].Expires = DateTime.Now.AddYears(-1);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}