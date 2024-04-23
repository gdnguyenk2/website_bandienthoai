using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webbandienthoai.Models;
using PagedList;
using static System.Net.WebRequestMethods;
using System.Web.Configuration;

namespace webbandienthoai.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();

        [ChildActionOnly]
        //tạo partial để hiển thị style
        public ActionResult SanPhamPatial()
        {
            //sp moi
            var lstMoi = db.SanPhams.Where(m => m.Moi == true && m.DaXoa == false).ToList();
            ViewBag.Moi = lstMoi;
            //sp bán chạy
            return PartialView(lstMoi);
        }
        [ChildActionOnly]
        public ActionResult SanPhamPatial2()
        {
            //sp moi
            var lstBanChay = db.SanPhams.Where(m => m.BanChay == true && m.DaXoa == false).ToList();
            ViewBag.BanChay = lstBanChay;
            //sp bán chạy
            return PartialView(lstBanChay);
        }

        public ActionResult ChiTietSanPham(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SanPham product = db.SanPhams.Where(row => row.MaSP == id && row.DaXoa == false).SingleOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            LoaiSanPham loaiid= db.LoaiSanPhams.SingleOrDefault(m=>m.MaLoaiSP ==product.MaLoaiSP);
            List<SanPham> sp_tuongtu = db.SanPhams.Where(m => m.MaLoaiSP == loaiid.MaLoaiSP).ToList();
            ViewBag.sptuongtu= sp_tuongtu;
            ViewBag.masp = id;
            return View(product);
        }
        public ActionResult SanPhamAll()
        {
            return View();
        }
        public ActionResult hienThiLoaiSP(int? MaNSX, int? page)
        {
            if(MaNSX == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSP = db.SanPhams.Where(n =>n.MaNSX == MaNSX);
            if(lstSP.Count() == 0)
            {
                return HttpNotFound();
            }
            if(Request.HttpMethod != "GET")
            {
                page = 1;
            }
            //thực hiện phân trang sản phẩm
            //Tạo biến số sản phẩm trên trang
            int PageSize = 6;
            //Tạo biến thứ 2 : số trang hiện tại
            int PageNumber = (page ?? 1);
            ViewBag.MaNSX = MaNSX;

            return View(lstSP.OrderBy(n=>n.MaSP).ToPagedList(PageNumber,PageSize));
        }
    }
}