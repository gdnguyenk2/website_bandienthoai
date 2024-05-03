using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using webbandienthoai.Models;

namespace webbandienthoai.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PhanQuyenController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        // GET: PhanQuyen
        public ActionResult Index()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            if (tv == null)
            {
                return RedirectToAction("DangNhap", "Login");
            }
            else
            {
                int currentMemberId = tv.MaTV;
                ViewBag.MaTV = tv.MaTV;
                List<ThanhVien> listMember = db.ThanhViens.ToList();
                return View(listMember);
            }
        }
        public ActionResult CapQuyen(int? MaTV)
        {
            ViewBag.Member = db.ThanhViens.Where(row => row.MaTV == MaTV).SingleOrDefault();

            return View();
        }
        [HttpPost]
        public ActionResult CapQuyen(ThanhVien tv)
        {

            ThanhVien tvUpdate = db.ThanhViens.SingleOrDefault(row => row.MaTV == tv.MaTV);

            tvUpdate.MaLoaiTV = tv.MaLoaiTV;
            db.SaveChanges();
            TempData["capquyen"] = "Cấp quyền thành công!";
            return RedirectToAction("Index", "PhanQuyen");
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoans"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}