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
            return PartialView(lstMoi.OrderByDescending(n=>n.NgayCapNhat));
        }
        [ChildActionOnly]
        public ActionResult SanPhamPatial2()
        {
            //sp bán chạy
            List<SanPham> lstsp = new List<SanPham>();
            var lstBanChay = db.SanPhams.Where(m=> m.DaXoa == false).ToList();
            foreach(var item in lstBanChay)
            {
                if(item.SoLanMua > 5)
                {
                    lstsp.Add(item);
                }
                else
                {
                    continue;
                }
            }
            return PartialView(lstsp.OrderByDescending(n=>n.SoLanMua));
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

            var dg = db.BinhLuans.Where(n => n.MaSP == id);
            int tongdg = 0;
            int danhgia1s = 0;
            int danhgia2s = 0;
            int danhgia3s = 0;
            int danhgia4s = 0;
            int danhgia5s = 0;
            foreach (var item in dg)
            {
                if (item.DanhGia == 1)
                {
                    danhgia1s++;
                }
                if (item.DanhGia == 2)
                {
                    danhgia2s++;
                }
                if (item.DanhGia == 3)
                {
                    danhgia3s++;
                }
                if (item.DanhGia == 4)
                {
                    danhgia4s++;
                }
                if (item.DanhGia == 5)
                {
                    danhgia5s++;
                }
                tongdg++;
            }
            ViewBag.tongdg = tongdg;
            ViewBag.danhgia1s = danhgia1s;
            ViewBag.danhgia2s = danhgia2s;
            ViewBag.danhgia3s = danhgia3s;
            ViewBag.danhgia4s = danhgia4s;
            ViewBag.danhgia5s = danhgia5s;
            return View(product);
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
        public ActionResult htHangSX(int? MaNSX)
        {
            var lstsp = db.SanPhams.ToList();
            return PartialView(lstsp);
        }
        public ActionResult BanChayPhu()
        {
            List<SanPham> lstsp = new List<SanPham>();
            var lstBanChay = db.SanPhams.Where(m => m.DaXoa == false).ToList();
            foreach (var item in lstBanChay)
            {
                if (item.SoLanMua > 5)
                {
                    lstsp.Add(item);
                }
                else
                {
                    continue;
                }
            }
            return PartialView(lstsp.OrderByDescending(n=>n.SoLanMua));
        }
    }
}