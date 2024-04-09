using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult DangNhap(FormCollection f)
        {
            //kiểm tra tên đăng nhập và mật khẩu
            string tk = f["TaiKhoan"].ToString();
            string mk = f["MatKhau"].ToString();

            ThanhVien tv = db.ThanhViens.SingleOrDefault(n=>n.TaiKhoan==tk && n.MatKhau==mk);

            if(tv!=null)
            {
                Session["TaiKhoans"] = tv;
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("DangNhap");
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoans"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}