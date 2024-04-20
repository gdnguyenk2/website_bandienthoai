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
        // GET: GioHang
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities(); 
        //Lấy giỏ hàng
        public List<GioHangItem> LayGioHang()
        {
            //Giỏ hàng đã tồn tại
            List<GioHangItem> lstGioHang = Session["GioHang"] as List<GioHangItem>;
            if (lstGioHang == null )
            {
                //Nếu giot session giỏ hàng chưa tồn tại thì khởi tạo giỏ hàng
                lstGioHang = new List<GioHangItem>();
                Session["GioHang"] = lstGioHang;
                return lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int MaSP, string Url)
        {
            //kiểm tra sản phẩm có tồn tại trong csdl không
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if(sp == null)
            {
                //Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            List<GioHangItem> lstGioHang = LayGioHang();
            GioHangItem spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if(spCheck == null)
            {
                GioHangItem itemGH = new GioHangItem(MaSP);
                if (sp.SoLuongTon < itemGH.SoLuong)
                {
                    return View("ThongBao");
                }
                lstGioHang.Add(itemGH);
            }
            //Sản phẩm đã tồn tại trong giỏ hàng
            
            else
            {
                if (sp.SoLuongTon < spCheck.SoLuong)
                {
                    return View("ThongBao");
                }
                spCheck.SoLuong=spCheck.SoLuong+1;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(Url);
            }
            
            return Redirect(Url);
        }
        public int TongSoLuong()
        {
            List<GioHangItem> lstGioHang = Session["GioHang"] as List<GioHangItem>;
            if(lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);  
        }
        public decimal TongThanhTien()
        {
            List<GioHangItem> lstGioHang = Session["GioHang"] as List<GioHangItem>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);
        }
        public ActionResult XemGioHang()
        {
            List<GioHangItem> lstGioHang = LayGioHang();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(lstGioHang);
        }
        [ChildActionOnly]
        public ActionResult GioHangIcon()
        {
            if (TongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            return PartialView();
        }
        //Chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang(int MaSP)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ session
            List<GioHangItem> listGioHang = LayGioHang();
            //kiểm tra sản phẩm có toonf tại trong giỏ hàng hay chưa
            GioHangItem spCheck = listGioHang.FirstOrDefault(n=>n.MaSP==MaSP);
            if(spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //lấy List giỏ hàng tạo giao diện
            ViewBag.GioHang = listGioHang;
            ViewBag.TongThanhTien = TongThanhTien();
            //Nếu tồn tại rồi
            return View(spCheck);
        }
        [HttpPost]
        public ActionResult CapNhatGioHang(GioHangItem itemGH)
        {
            //kiểm tra số lượng tồn
            SanPham spCheck = db.SanPhams.SingleOrDefault(n => n.MaSP == itemGH.MaSP);
            if (spCheck.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            //Cập nhật số lượng trong session giỏ hang
            //Lấy List<GioHang> từ sessin[Giohang]
            List<GioHangItem> lstGH = LayGioHang();
            //Lấy sản phẩm cần cập nhật từ trong list<GioHang> ra
            GioHangItem itemGHUpdate = lstGH.Find(n => n.MaSP == itemGH.MaSP);
            //tiến hành cập nhật lại số lượng cộng thành tiền
            itemGHUpdate.SoLuong = itemGH.SoLuong;
            itemGHUpdate.ThanhTien = itemGHUpdate.SoLuong * itemGHUpdate.DonGia;
            return RedirectToAction("XemGioHang", new {@MaSP=itemGH.MaSP});
        }
        public ActionResult XoaGioHang(int MaSP)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ session
            List<GioHangItem> listGioHang = LayGioHang();
            //kiểm tra sản phẩm có toonf tại trong giỏ hàng hay chưa
            GioHangItem spCheck = listGioHang.FirstOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Xóa item trong giỏ hàng
            listGioHang.Remove(spCheck);
            return RedirectToAction("XemGioHang", "GioHang");
        }
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang(KhachHang kh)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KhachHang khang= new KhachHang();
            if (Session["TaiKhoans"] == null)
            {
                //thêm khách hàng vào bảng khách hàng chưa có tài khoản
                khang = kh;
                db.KhachHangs.Add(khang);
                db.SaveChanges();
            }
            else
            {
                ThanhVien tv= Session["TaiKhoans"] as ThanhVien;
                khang.TenKH = tv.HoTen;
                khang.DiaChi = kh.DiaChi;
                khang.Email=tv.Email;
                khang.SoDienThoai = tv.SoDienThoai;
                khang.MaTV = tv.MaLoaiTV;
                db.KhachHangs.Add(khang);
                db.SaveChanges();
            }
            //Thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = khang.MaKH;
            ddh.NgayDatHang=DateTime.Now;
            ddh.TinhTrang = "Chưa giao hàng";
            ddh.DaThanhToan = false;
            ddh.QuaTang = "không";
            ddh.DaXoa = false;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            //Thêm chi tiết đơn đặt hàng
            List<GioHangItem> lstGH = LayGioHang();
            foreach(var item in lstGH)
            {
                ChiTietDonDatHang ctdh = new ChiTietDonDatHang();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.MaSP = item.MaSP;
                ctdh.TenSP=item.TenSP;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia= item.DonGia;
                db.ChiTietDonDatHangs.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang","GioHang");
        }
    }
}