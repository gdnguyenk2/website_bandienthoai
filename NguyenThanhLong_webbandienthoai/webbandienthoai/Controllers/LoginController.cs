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
            // Tìm thành viên trong cơ sở dữ liệu với tên tài khoản và mật khẩu tương ứng
            ThanhVien tv = db.ThanhViens.Where(row => row.TaiKhoan == truycap.TaiKhoan && row.MatKhau == truycap.MatKhau).SingleOrDefault();

            // Nếu tìm thấy thành viên khớp
            if (tv != null)
            {
                // Lưu thông tin thành viên vào session
                Session["TaiKhoans"] = tv;

                // tạo cookie để nhớ người dùng
                if (RememberMe)
                {
                    HttpCookie cookie = new HttpCookie("RememberMeCookie");
                    cookie.Values["TaiKhoan"] = tv.TaiKhoan;
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookie);
                }
                else // nếu tồn tại rồi thì xóa bất kỳ cookie đã tồn tại
                {
                    if (Request.Cookies["RememberMeCookie"] != null)
                    {
                        Response.Cookies.Remove("RememberMeCookie");
                        Response.Cookies["RememberMeCookie"].Expires = DateTime.Now.AddYears(-1);
                    }
                }

                // Lấy danh sách các quyền liên quan đến loại thành viên
                IEnumerable<LoaiThanhVien_Quyen> listQuyen = db.LoaiThanhVien_Quyen.Where(row => row.MaLoaiTV == tv.MaLoaiTV);
                string CacQuyen = "";

                // Tạo chuỗi các quyền từ danh sách quyền
                foreach (var item in listQuyen)
                {
                    CacQuyen += item.Quyen.MaQuyen + ",";
                }

                // Xóa dấu phẩy cuối cùng trong chuỗi quyền
                CacQuyen = CacQuyen.Substring(0, CacQuyen.Length - 1);

                // Gán quyền cho tài khoản
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

            // Nếu tên tài khoản hoặc mật khẩu không đúng, hiển thị thông báo lỗi
            ViewBag.Loi = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }

        // Phân quyền cho tài khoản
        public void GrantPermissions(string tk, string quyen)
        {
            FormsAuthentication.Initialize();

            // Tạo vé xác thực với thông tin tài khoản và quyền
            var ticket = new FormsAuthenticationTicket(1, tk, DateTime.Now, DateTime.Now.AddHours(3), false, quyen, FormsAuthentication.FormsCookiePath);

            // Mã hóa vé xác thực và tạo cookie để lưu vé này
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));

            // Nếu vé là persistent (không hết hạn khi trình duyệt đóng), thiết lập thời gian hết hạn cho cookie
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

            // Thêm cookie vào phản hồi HTTP
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