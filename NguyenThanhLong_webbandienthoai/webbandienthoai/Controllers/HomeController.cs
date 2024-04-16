using System;
using System.Collections.Generic;
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
                    Session["TaiKhoans"] = member;
                }
            }

            return PartialView();
        }
        public ActionResult test()
        {
            return View();
        }
    }
}