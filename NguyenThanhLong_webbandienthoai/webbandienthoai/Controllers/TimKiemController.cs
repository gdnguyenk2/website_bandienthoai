using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbandienthoai.Models;
using PagedList;
using System.Web.UI;
namespace webbandienthoai.Controllers
{
    public class TimKiemController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        // GET: TimKiem
        public ActionResult KQTimKiem( string sTuKhoa, int? page)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            //thực hiện phân trang sản phẩm
            //Tạo biến số sản phẩm trên trang
            int PageSize = 6;
            //Tạo biến thứ 2 : số trang hiện tại
            int PageNumber = (page ?? 1);
            //Tìm kiếm theo tên sản phẩm
            var lstSP = db.SanPhams.Where(n=>n.TenSP.Contains(sTuKhoa) && n.DaXoa==false);
            ViewBag.TuKhoa = sTuKhoa;
            return View(lstSP.OrderBy(n=>n.TenSP).ToPagedList(PageNumber,PageSize));
        }
        [HttpPost]
        public ActionResult LayTuKhoaTimKiem(string sTuKhoa)
        {
            //gọi hàm get tìm kiếm
            return RedirectToAction("KQTimKiem", "TimKiem", new {@sTuKhoa=sTuKhoa});
        }
    }
}