using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using webbandienthoai.Models;
namespace webbandienthoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NhapHangController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        // GET: NhapHang
        public ActionResult NhapHang()
        {
            ViewBag.MaNCC = db.NhaCungCaps;
            ViewBag.ListSanPham = db.SanPhams;
            return View();
        }
        [HttpPost]
        public ActionResult NhapHang(PhieuNhap Model,IEnumerable<ChiTietPhieuNhap> lstModel)
        {
            //Sau khi kiểm tra dữ liệu đầu vào đúng
            ViewBag.MaNCC = db.NhaCungCaps;
            ViewBag.ListSanPham = db.SanPhams;
            //Gán đã xóa bằng false
            Model.DaXoa = false;
            db.PhieuNhaps.Add(Model);
            db.SaveChanges();
            //lấy MaPN để gán cho bên CHiTietPhieuNhap
            SanPham sp;
            foreach ( var item in lstModel)
            {
                //Cập nhật số lượng tồn trong sp
                sp = db.SanPhams.Single(n=>n.MaSP==item.MaSP);
                sp.SoLuongTon += item.SoLuongNhap;
                item.MaPN = Model.MaPN;
            }
            
            //thêm vào cơ sở dữ liệu với danh sách phương thức AddRange()
            db.ChiTietPhieuNhaps.AddRange(lstModel);
            db.SaveChanges();

            return View();
        }
        public ActionResult DSSapHetHang()
        {
            var lstSanPham = db.SanPhams.Where(n => n.DaXoa == false && n.SoLuongTon <= 5).ToList();
            return View(lstSanPham);
        }
        //Tạo View để nhập từng sản phẩm
        public ActionResult NhapHangDon(int? MaSP)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n=>n.TenNCC),"MaNCC","TenNCC");
            var sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (MaSP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        [HttpPost]
        public ActionResult NhapHangDon(PhieuNhap Model,ChiTietPhieuNhap oneModel)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC",Model.MaNCC);
            Model.DaXoa = false;
            db.PhieuNhaps.Add(Model);
            db.SaveChanges();
            //lấy MaPN để gán cho bên CHiTietPhieuNhap
            oneModel.MaPN = Model.MaPN;
            SanPham sp= db.SanPhams.Single(n => n.MaSP == oneModel.MaSP);
            //Cập nhật số lượng tồn trong sp
            sp.SoLuongTon += oneModel.SoLuongNhap;        
            //thêm vào cơ sở dữ liệu với danh sách phương thức AddRange()
            db.ChiTietPhieuNhaps.Add(oneModel);
            db.SaveChanges();
            return RedirectToAction("DSSapHetHang");

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);   
        }
    }
}