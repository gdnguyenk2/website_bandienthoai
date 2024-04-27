using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbandienthoai.Models;
namespace webbandienthoai.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        // GET: QuanLySanPham
        public ActionResult Index()
        {
            return View(db.SanPhams.Where(n=>n.DaXoa==false).OrderByDescending(n=>n.MaSP));
        }
        public ActionResult TaoMoi()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n=>n.TenNCC),"MaNCC","TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaKhuyenMai = new SelectList(db.KhuyenMais.OrderBy(n => n.MaKhuyenMai), "MaKhuyenMai", "TenKhuyenMai");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(SanPham sp,HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3, HttpPostedFileBase HinhAnh4)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaKhuyenMai = new SelectList(db.KhuyenMais.OrderBy(n => n.MaKhuyenMai), "MaKhuyenMai", "TenKhuyenMai");
            //Kiểm tra hình có tồn tại trong csdl ko
            if (HinhAnh != null && HinhAnh2 != null && HinhAnh3 != null && HinhAnh4 != null)
            {
                if (HinhAnh.ContentLength > 0)
                {
                    //Lấy tên hình ảnh
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    //Lấy hình ảnh chuyển vào thư mục hình ảnh
                    var path = Path.Combine(Server.MapPath("~/Content/Images/Products"), fileName);
                    //Nếu thư mục có hình ảnh rồi thì thông báo
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.upload = "Hình đã tồn tại";
                        return View(sp);
                    }
                    else
                    {
                        //Lấy hình ảnh đưa vào thư mục
                        HinhAnh.SaveAs(path);
                        sp.HinhAnh = fileName;
                    }
                }
                if (HinhAnh2.ContentLength > 0)
                {
                    //Lấy tên hình ảnh
                    var fileName2 = Path.GetFileName(HinhAnh2.FileName);
                    //Lấy hình ảnh chuyển vào thư mục hình ảnh
                    var path2 = Path.Combine(Server.MapPath("~/Content/Images/Products"), fileName2);
                    //Nếu thư mục có hình ảnh rồi thì thông báo
                    if (System.IO.File.Exists(path2))
                    {
                        ViewBag.upload2 = "Hình đã tồn tại";
                        return View(sp);
                    }
                    else
                    {
                        //Lấy hình ảnh đưa vào thư mục
                        HinhAnh2.SaveAs(path2);
                        sp.HinhAnh2 = fileName2;
                    }
                }
                if (HinhAnh3.ContentLength > 0)
                {
                    //Lấy tên hình ảnh
                    var fileName3 = Path.GetFileName(HinhAnh3.FileName);
                    //Lấy hình ảnh chuyển vào thư mục hình ảnh
                    var path3 = Path.Combine(Server.MapPath("~/Content/Images/Products"), fileName3);
                    //Nếu thư mục có hình ảnh rồi thì thông báo
                    if (System.IO.File.Exists(path3))
                    {
                        ViewBag.upload3 = "Hình đã tồn tại";
                        return View(sp);
                    }
                    else
                    {
                        //Lấy hình ảnh đưa vào thư mục
                        HinhAnh3.SaveAs(path3);
                        sp.HinhAnh3 = fileName3;
                    }
                }
                if (HinhAnh4.ContentLength > 0)
                {
                    //Lấy tên hình ảnh
                    var fileName4 = Path.GetFileName(HinhAnh4.FileName);
                    //Lấy hình ảnh chuyển vào thư mục hình ảnh
                    var path4 = Path.Combine(Server.MapPath("~/Content/Images/Products"), fileName4);
                    //Nếu thư mục có hình ảnh rồi thì thông báo
                    if (System.IO.File.Exists(path4))
                    {
                        ViewBag.upload4 = "Hình đã tồn tại";
                        return View(sp);
                    }
                    else
                    {
                        //Lấy hình ảnh đưa vào thư mục
                        HinhAnh4.SaveAs(path4);
                        sp.HinhAnh4 = fileName4;
                    }
                }
            }
            else
            {
                return View(sp);
            }
            db.SanPhams.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Index","QuanLySanPham");
        }
        public ActionResult SuaSP(int? id)
        {
            if(id== null){
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n=>n.MaSP==id);
            if (sp==null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC",sp.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX",sp.MaNSX);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP",sp.MaLoaiSP);
            ViewBag.MaKhuyenMai = new SelectList(db.KhuyenMais.OrderBy(n => n.MaKhuyenMai), "MaKhuyenMai", "TenKhuyenMai",sp.MaKhuyenMai);
            return View(sp);

        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SuaSP(SanPham sp, HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3, HttpPostedFileBase HinhAnh4)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sp.MaNSX);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP", sp.MaLoaiSP);
            ViewBag.MaKhuyenMai = new SelectList(db.KhuyenMais.OrderBy(n => n.MaKhuyenMai), "MaKhuyenMai", "TenKhuyenMai", sp.MaKhuyenMai);
            SanPham product = db.SanPhams.Where(row => row.MaSP == sp.MaSP).SingleOrDefault();

            product.MaNCC = sp.MaNCC;
            product.MaNSX = sp.MaNSX;
            product.MaLoaiSP = sp.MaLoaiSP;
            product.TenSP = sp.TenSP;
            product.MaKhuyenMai = sp.MaKhuyenMai;
            product.NgayCapNhat = sp.NgayCapNhat;
            product.SoLuongTon = sp.SoLuongTon;
            product.MoTa = sp.MoTa;
            product.CauHinh = sp.CauHinh;
            product.MoTa = sp.MoTa;
            product.BanChay = sp.BanChay;
            product.DaXoa = sp.DaXoa;
            if (HinhAnh != null && HinhAnh2 != null && HinhAnh3 != null && HinhAnh4 != null)
            {
                if (HinhAnh.ContentLength > 0)
                {
                    //Lấy tên hình ảnh
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    //Lấy hình ảnh chuyển vào thư mục hình ảnh
                    var path = Path.Combine(Server.MapPath("~/Content/Images/Products"), fileName);
                    //Nếu thư mục có hình ảnh rồi thì thông báo
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.upload = "Hình đã tồn tại";
                        return View(sp);
                    }
                    else
                    {
                        //Lấy hình ảnh đưa vào thư mục
                        HinhAnh.SaveAs(path);
                        sp.HinhAnh = fileName;
                        product.HinhAnh = sp.HinhAnh;
                    }
                }
                if (HinhAnh2.ContentLength > 0)
                {
                    //Lấy tên hình ảnh
                    var fileName2 = Path.GetFileName(HinhAnh2.FileName);
                    //Lấy hình ảnh chuyển vào thư mục hình ảnh
                    var path2 = Path.Combine(Server.MapPath("~/Content/Images/Products"), fileName2);
                    //Nếu thư mục có hình ảnh rồi thì thông báo
                    if (System.IO.File.Exists(path2))
                    {
                        ViewBag.upload2 = "Hình đã tồn tại";
                        return View(sp);
                    }
                    else
                    {
                        //Lấy hình ảnh đưa vào thư mục
                        HinhAnh2.SaveAs(path2);
                        sp.HinhAnh2 = fileName2;
                        product.HinhAnh2 = sp.HinhAnh2;
                    }
                }
                if (HinhAnh3.ContentLength > 0)
                {
                    //Lấy tên hình ảnh
                    var fileName3 = Path.GetFileName(HinhAnh3.FileName);
                    //Lấy hình ảnh chuyển vào thư mục hình ảnh
                    var path3 = Path.Combine(Server.MapPath("~/Content/Images/Products"), fileName3);
                    //Nếu thư mục có hình ảnh rồi thì thông báo
                    if (System.IO.File.Exists(path3))
                    {
                        ViewBag.upload3 = "Hình đã tồn tại";
                        return View(sp);
                    }
                    else
                    {
                        //Lấy hình ảnh đưa vào thư mục
                        HinhAnh3.SaveAs(path3);
                        sp.HinhAnh3 = fileName3;
                        product.HinhAnh3 = sp.HinhAnh3;
                    }
                }
                if (HinhAnh4.ContentLength > 0)
                {
                    //Lấy tên hình ảnh
                    var fileName4 = Path.GetFileName(HinhAnh4.FileName);
                    //Lấy hình ảnh chuyển vào thư mục hình ảnh
                    var path4 = Path.Combine(Server.MapPath("~/Content/Images/Products"), fileName4);
                    //Nếu thư mục có hình ảnh rồi thì thông báo
                    if (System.IO.File.Exists(path4))
                    {
                        ViewBag.upload4 = "Hình đã tồn tại";
                        return View(sp);
                    }
                    else
                    {
                        //Lấy hình ảnh đưa vào thư mục
                        HinhAnh4.SaveAs(path4);
                        sp.HinhAnh4 = fileName4;
                        product.HinhAnh4 = sp.HinhAnh4;
                    }
                }
            }
            else
            {
                product.HinhAnh = product.HinhAnh;
                product.HinhAnh2 = product.HinhAnh2;
                product.HinhAnh3 = product.HinhAnh3;
                product.HinhAnh4 = product.HinhAnh4;
            }
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult XoaSP(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sp.MaNSX);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP", sp.MaLoaiSP);
            ViewBag.MaKhuyenMai = new SelectList(db.KhuyenMais.OrderBy(n => n.MaKhuyenMai), "MaKhuyenMai", "TenKhuyenMai", sp.MaKhuyenMai);
            return View(sp);
        }
        [HttpPost]
        public ActionResult XoaSP(int MaSP)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(row => row.MaSP == MaSP);

            if (sp == null)
            {
                return HttpNotFound();
            }

            sp.DaXoa = true;

            db.SanPhams.Remove(sp);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}