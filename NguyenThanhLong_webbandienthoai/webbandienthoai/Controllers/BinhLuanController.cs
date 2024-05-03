using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using webbandienthoai.Models;

namespace webbandienthoai.Controllers
{
    public class BinhLuanController : Controller
    {
        // GET: BinhLuan
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        [ChildActionOnly]
        public ActionResult Form_BinhLuan(int MaSP)
        {
            ViewBag.MaSP = MaSP;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Form_BinhLuan(BinhLuan bl)
        {
            if (ModelState.IsValid)
            {
                bl.NgayBL=DateTime.Now;
                db.BinhLuans.Add(bl);
                db.SaveChanges();
                return Json(new {Success=true});
            }
            return Json(new { Success = false });
        }
        public ActionResult ds_BinhLuan(int? MaSP)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bl = db.BinhLuans.Where(n => n.MaSP == MaSP).ToList();
            if (bl.Count() == 0)
            {
                return HttpNotFound();
            }
            return PartialView(bl.OrderByDescending(n => n.MaBinhLuan));
        }
        public ActionResult tk_BinhLuan(int MaSP)
        {
            var dg = db.BinhLuans.Where(n => n.MaSP == MaSP);
            var sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
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
            float danhgiatb = 0;
            int nguyendg = 0;
            if (tongdg == 0)
            {
                nguyendg = 0;
            }
            else
            {
                danhgiatb = (float)(ViewBag.danhgia1s * 1 + ViewBag.danhgia2s * 2 + ViewBag.danhgia3s * 3 + ViewBag.danhgia4s * 4 + ViewBag.danhgia5s * 5) / ViewBag.tongdg;
                nguyendg = Convert.ToInt32(danhgiatb);
                sp.DanhGia = nguyendg;
                db.SaveChanges();
            }
            ViewBag.danhgiatb = danhgiatb;
            ViewBag.nguyendg = nguyendg;
            return PartialView();
        }
        
    }
}