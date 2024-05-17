﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbandienthoai.Models;
namespace webbandienthoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyQuangCaoController : Controller
    {
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities();
        // GET: QuanLyQuangCao
        public ActionResult Index()
        {
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            if (tv != null)
            {
                return View(db.QuangCaos.OrderByDescending(n => n.NgayCapNhat));
            }
            else
            {
                return RedirectToAction("DangNhap","Login");
            }
        }
        public ActionResult ThemQuangCao()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemQuangCao(QuangCao qc, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                //Kiểm tra hình có tồn tại trong csdl ko
                if (HinhAnh != null)
                {
                    if (HinhAnh.ContentLength > 0)
                    {
                        //Lấy tên hình ảnh
                        var fileName = Path.GetFileName(HinhAnh.FileName);
                        //Lấy hình ảnh chuyển vào thư mục hình ảnh
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Banners"), fileName);
                        //Nếu thư mục có hình ảnh rồi thì thông báo
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.upload = "Hình đã tồn tại";
                            return View(qc);
                        }
                        else
                        {
                            //Lấy hình ảnh đưa vào thư mục
                            HinhAnh.SaveAs(path);
                            qc.HinhAnh = fileName;
                        }
                    }
                }
                else
                {
                    return View(qc);
                }
                db.QuangCaos.Add(qc);
                db.SaveChanges();
                TempData["themthanhcong"] = "Thêm quảng cáo thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(qc);
            }

        }
        public ActionResult XoaQC(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            QuangCao qc = db.QuangCaos.SingleOrDefault(n => n.MaQuangCao == id);
            if (qc == null)
            {
                return HttpNotFound();
            }
            return View(qc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaQC(int MaQuangCao)
        {
            QuangCao qc = db.QuangCaos.SingleOrDefault(n => n.MaQuangCao == MaQuangCao);

            if (qc == null)
            {
                return HttpNotFound();
            }

            db.QuangCaos.Remove(qc);
            db.SaveChanges();
            TempData["xoathanhcong"] = "Xóa quảng cáo thành công!";
            return RedirectToAction("Index");
        }
    }
}