using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbandienthoai.Models;

namespace webbandienthoai.Controllers
{
    public class GioHangController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        // GET: GioHang
        //Lấy giỏ hàng
        public List<GioHangItem> LayGioHang()
        {
            if (Session["TaiKhoans"] == null)
            {
                return null;
            }

            ThanhVien member = Session["TaiKhoans"] as ThanhVien;

            if (db.GioHangs.Any(row => row.MaTV == member.MaTV))
            {
                List<GioHangItem> listCart = new List<GioHangItem>();

                int cartId = db.GioHangs.Where(row => row.MaTV == member.MaTV).Select(row => row.MaGioHang).SingleOrDefault();

                List<ChiTietGioHang> listCartDetail = db.ChiTietGioHangs.Where(row => row.MaGioHang == cartId).ToList();

                foreach (ChiTietGioHang cartDetail in listCartDetail)
                {
                    GioHangItem cartItem = new GioHangItem(cartDetail.MaSP, cartDetail.DonGia, cartDetail.SoLuong, cartDetail.MaMau);

                    listCart.Add(cartItem);
                }

                return listCart;
            }
            else
            {
                return null;
            }
        }
        public ActionResult ThemGioHang(int MaSP, decimal GiaBan, int SoLuong, int MaMau, string Url)
        {
            Session["QuantityExceeded"] = null;

            SanPham product = db.SanPhams.Where(row => row.MaSP == MaSP).SingleOrDefault();

            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            ThanhVien member = Session["TaiKhoans"] as ThanhVien;

            List<GioHangItem> listCart = LayGioHang();

            if (listCart == null)
            {
                GioHang cart = new GioHang();

                cart.MaTV = member.MaTV;

                db.GioHangs.Add(cart);
                db.SaveChanges();

                listCart = new List<GioHangItem>();
            }

            int cartId = db.GioHangs.Where(row => row.MaTV == member.MaTV).Select(row => row.MaGioHang).SingleOrDefault();

            GioHangItem productCheck = listCart.Where(row => row.MaSP == MaSP && row.MaMau == MaMau).SingleOrDefault();

            if (productCheck != null)
            {
                if (productCheck.SoLuong + SoLuong > 5)
                {
                    Session["QuantityExceeded"] = true;

                    return Redirect(Url);
                }

                if (productCheck.SoLuong + SoLuong <= product.SoLuongTon)
                {
                    productCheck.SoLuong += SoLuong;
                    productCheck.ThanhTien = productCheck.SoLuong * productCheck.DonGia;

                    ChiTietGioHang cartDetail = db.ChiTietGioHangs.Where(row => row.MaSP == MaSP && row.MaMau == MaMau && row.MaGioHang == cartId).SingleOrDefault();

                    cartDetail.SoLuong = productCheck.SoLuong;

                    db.SaveChanges();

                    return Redirect(Url);
                }
                else
                {
                    return RedirectToAction("ThongBaoHetHang", "GioHang");
                }
            }
            else
            {
                if (SoLuong <= product.SoLuongTon)
                {
                    GioHangItem cartItem = new GioHangItem(MaSP, GiaBan, SoLuong, MaMau);
                    listCart.Add(cartItem);

                    ChiTietGioHang cartDetail = new ChiTietGioHang();

                    cartDetail.MaGioHang = cartId;
                    cartDetail.MaSP = MaSP;
                    cartDetail.SoLuong = SoLuong;
                    cartDetail.DonGia = GiaBan;
                    cartDetail.MaMau = MaMau;

                    db.ChiTietGioHangs.Add(cartDetail);
                    db.SaveChanges();

                    return Redirect(Url);
                }
                else
                {
                    return RedirectToAction("ThongBaoHetHang", "GioHang");
                }
            }
        }
        public ActionResult ThongBaoHetHang()
        {
            return View();
        }
        public double TongSoLuong()
        {
            List<GioHangItem> listCart = LayGioHang();

            if (listCart == null)
            {
                return 0;
            }

            return listCart.Sum(row => row.SoLuong);
        }

        public decimal TongThanhTien()
        {
            List<GioHangItem> listCart = LayGioHang();

            if (listCart == null)
            {
                return 0;
            }

            return listCart.Sum(row => row.ThanhTien);
        }
        public ActionResult XemGioHang()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult GioHangIcon()
        {
            if (TongSoLuong() == 0)
            {
                ViewBag.Count = 0;

                return PartialView();
            }

            ViewBag.Count = TongSoLuong();

            return PartialView();
        }
    }
}