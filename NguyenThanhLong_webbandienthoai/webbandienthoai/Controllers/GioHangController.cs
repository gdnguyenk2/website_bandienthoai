using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using webbandienthoai.Models;
using webbandienthoai.Models.vnpayments;

namespace webbandienthoai.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        WebBanDienThoaiEntities db = new WebBanDienThoaiEntities(); 
        //Lấy giỏ hàng
        public List<GioHangItem> LayGioHang()
        {
            if (Session["TaiKhoans"] == null)
            {
                return null;
            }

            ThanhVien member = Session["TaiKhoans"] as ThanhVien;
            //Giỏ hàng đã tồn tại
            if (db.GioHangs.Any(row => row.MaTV == member.MaTV))
            {
                List<GioHangItem> listCart = new List<GioHangItem>();

                int cartId = db.GioHangs.Where(row => row.MaTV == member.MaTV).Select(row => row.MaGioHang).SingleOrDefault();

                List<ChiTietGioHang> listCartDetail = db.ChiTietGioHangs.Where(row => row.MaGioHang == cartId).ToList();

                foreach (ChiTietGioHang cartDetail in listCartDetail)
                {
                    
                    SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == cartDetail.MaSP);
                    if (cartDetail.SoLuong > sp.SoLuongTon)
                    {
                        cartDetail.SoLuong = sp.SoLuongTon;
                    }
                    else if (cartDetail.SoLuong==0)
                    {
                        db.ChiTietGioHangs.Remove(cartDetail);
                        continue;
                    }
                    GioHangItem cartItem = new GioHangItem(cartDetail.MaSP, cartDetail.SoLuong);
                    listCart.Add(cartItem);
                }
                return listCart;
            }
            else
            {
                return null;
            }
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int iMaSP,int sl)
        {
            //kiểm tra sản phẩm có tồn tại trong csdl không
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == iMaSP);
            if(sp == null)
            {
                //Trang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return Json(new { success = false,message = "Sản phẩm không tồn tại"},JsonRequestBehavior.AllowGet);
            }
            ThanhVien member = Session["TaiKhoans"] as ThanhVien;
            List<GioHangItem> lstGioHang = LayGioHang();
            if (lstGioHang == null)
            {
                GioHang cart = new GioHang();

                cart.MaTV = member.MaTV;

                db.GioHangs.Add(cart);
                db.SaveChanges();

                lstGioHang = new List<GioHangItem>();
            }
            int cartId = db.GioHangs.Where(row => row.MaTV == member.MaTV).Select(row => row.MaGioHang).SingleOrDefault();

            GioHangItem productCheck = lstGioHang.Where(row => row.MaSP == iMaSP).SingleOrDefault();
            //Sản phẩm đã tồn tại trong giỏ hàng
            if (productCheck != null)
            {

                if (productCheck.SoLuong + sl <= sp.SoLuongTon)
                {
                    productCheck.SoLuong += sl;
                    productCheck.ThanhTien = productCheck.SoLuong * productCheck.DonGia;

                    ChiTietGioHang cartDetail = db.ChiTietGioHangs.Where(row => row.MaSP == iMaSP && row.MaGioHang == cartId).SingleOrDefault();

                    cartDetail.SoLuong = productCheck.SoLuong;

                    db.SaveChanges();
                    return Json(new { success = true, message = "Thêm vào giỏ hàng thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Sản phẩm đã hết hàng" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (sl <= sp.SoLuongTon)
                {
                    GioHangItem cartItem = new GioHangItem(iMaSP,sl);
                    lstGioHang.Add(cartItem);

                    ChiTietGioHang cartDetail = new ChiTietGioHang();

                    cartDetail.MaGioHang = cartId;
                    cartDetail.MaSP = iMaSP;
                    cartDetail.SoLuong = sl;

                    db.ChiTietGioHangs.Add(cartDetail);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Thêm vào giỏ hàng thành công" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Sản phẩm đã hết hàng" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public int TongSoLuong()
        {
            List<GioHangItem> lstGioHang = LayGioHang();
            if(lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);  
        }
        public decimal TongThanhTien()
        {
            List<GioHangItem> lstGioHang = LayGioHang();
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
            if (LayGioHang() == null)
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
            ThanhVien member = Session["TaiKhoans"] as ThanhVien;

            int cartId = db.GioHangs.Where(row => row.MaTV == member.MaTV).Select(row => row.MaGioHang).SingleOrDefault();

            ChiTietGioHang cartDetail = db.ChiTietGioHangs.Where(row => row.MaSP == itemGH.MaSP && row.MaGioHang == cartId).SingleOrDefault();

            cartDetail.SoLuong = itemGHUpdate.SoLuong;
            db.SaveChanges();
            return RedirectToAction("XemGioHang", new {@MaSP=itemGH.MaSP});
        }
        public ActionResult XoaGioHang(int MaSP)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa
            if (LayGioHang() == null)
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
            ThanhVien member = Session["TaiKhoans"] as ThanhVien;

            int cartId = db.GioHangs.Where(row => row.MaTV == member.MaTV).Select(row => row.MaGioHang).SingleOrDefault();

            ChiTietGioHang cartDetail = db.ChiTietGioHangs.Where(row => row.MaSP == MaSP && row.MaGioHang == cartId).SingleOrDefault();

            db.ChiTietGioHangs.Remove(cartDetail);

            db.SaveChanges();

            return RedirectToAction("XemGioHang", "GioHang");
        }
        public ActionResult PaymentReturn()
        {
            // Kiểm tra nếu có dữ liệu truy vấn được gửi từ VnPay
            if (Request.QueryString.Count > 0)
            {
                // Lấy giá trị của "vnp_HashSecret" từ cấu hình (web.config hoặc app.config)
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];
                // Lấy tất cả các dữ liệu truy vấn (query string) từ request
                var vnpayData = Request.QueryString;

                // Khởi tạo đối tượng VnPayLibrary
                VnPayLibrary vnpay = new VnPayLibrary();
                // Lặp qua tất cả các tham số trong query string
                foreach (string s in vnpayData)
                {
                    // Nếu tham số không rỗng và bắt đầu với "vnp_"
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        // Thêm tham số và giá trị của nó vào đối tượng VnPayLibrary
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }

                // Kiểm tra chữ ký bảo mật để xác minh tính toàn vẹn của dữ liệu
                bool checkSignature = vnpay.ValidateSignature(vnpayData["vnp_SecureHash"], vnp_HashSecret);

                // Nếu chữ ký hợp lệ
                if (checkSignature)
                {
                    // Thanh toán đã được xử lý thành công
                    // Cập nhật trạng thái đơn hàng và xử lý logic nghiệp vụ
                    // Chuyển hướng người dùng đến trang thanh toán thành công
                    return RedirectToAction("paymentsuccess", "GioHang");
                }
                else
                {
                    // Nếu chữ ký không hợp lệ, hiển thị thông báo lỗi
                    ViewBag.Message = "Chữ ký không hợp lệ!";
                }
            }
            else
            {
                // Nếu không có dữ liệu truy vấn nào được gửi từ VnPay, hiển thị thông báo lỗi
                ViewBag.Message = "Không nhận được dữ liệu!";
            }

            // Trả về view hiện tại với thông báo lỗi nếu có
            return View();

        }
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang(KhachHang kh,string DaThanhToan)
        {
            
            if (LayGioHang() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ThanhVien tv = Session["TaiKhoans"] as ThanhVien;
            KhachHang khang= new KhachHang();
            khang.TenKH = kh.TenKH;
            khang.DiaChi = kh.DiaChi;
            khang.Email=kh.Email;
            khang.SoDienThoai = kh.SoDienThoai;
            khang.MaTV = tv.MaTV;
            db.KhachHangs.Add(khang);
            db.SaveChanges();
            //Thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = khang.MaKH;
            ddh.NgayDatHang=DateTime.Now;
            ddh.TinhTrang = "Chưa phê duyệt";
            ddh.DaThanhToan = DaThanhToan;
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

                SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == item.MaSP);
                if (sp != null) // Kiểm tra xem sản phẩm có tồn tại không
                {
                    sp.SoLuongTon -= item.SoLuong; // Giảm số lượng tồn đi
                }
            }
            //gửi mail cho khách hàng đặt hàng thành công
            string strSanPham = "";
            var thanhtien = decimal.Zero;
            var tenKh = khang.TenKH;
            var sdt = khang.SoDienThoai;
            var email = khang.Email;
            var diachi = khang.DiaChi;
            var index = 1;
            foreach (var item in lstGH)
            {
                strSanPham += "<tr>";
                strSanPham += "<td>" + index + "</td>";
                strSanPham += "<td>" + item.TenSP + " đ</td>";
                strSanPham += "<td>" + item.SoLuong + " đ</td>";
                strSanPham += "<td>" + item.DonGia.ToString("#,##") + " đ</td>";
                strSanPham += "</tr>";
                thanhtien += item.SoLuong * item.DonGia;
                index++;
            }
            var TongTien = thanhtien;
            string contentPath = Server.MapPath("~/Common/send2.html");
            string content = System.IO.File.ReadAllText(contentPath);
            content = content.Replace("{{MaDon}}", ddh.MaDDH.ToString());
            content = content.Replace("{{NgayDat}}", ddh.NgayDatHang.ToString());
            content = content.Replace("{{SanPham}}", strSanPham);
            content = content.Replace("{{ThanhTien}}", thanhtien.ToString("#,##"));
            content = content.Replace("{{TongTien}}", TongTien.ToString("#,##"));
            content = content.Replace("{{TenKH}}", tenKh);
            content = content.Replace("{{SoDienThoai}}", sdt);
            content = content.Replace("{{Email}}", email);
            content = content.Replace("{{DiaChi}}", diachi);
            GuiMail("Đơn đặt hàng từ DragonPhone", email, "dragonphone17@gmail.com", "tgarnlbhgqedgzpt", content);
            string contentPath2 = Server.MapPath("~/Common/send1.html");
            string content2 = System.IO.File.ReadAllText(contentPath2);
            content2 = content2.Replace("{{MaDon}}", ddh.MaDDH.ToString());
            content2 = content2.Replace("{{NgayDat}}", ddh.NgayDatHang.ToString());
            content2 = content2.Replace("{{SanPham}}", strSanPham);
            content2 = content2.Replace("{{ThanhTien}}", thanhtien.ToString("#,##"));
            content2 = content2.Replace("{{TongTien}}", TongTien.ToString("#,##"));
            content2 = content2.Replace("{{TenKH}}", tenKh);
            content2 = content2.Replace("{{SoDienThoai}}", sdt);
            content2 = content2.Replace("{{Email}}", email);
            content2 = content2.Replace("{{DiaChi}}", diachi);
            GuiMail("Đơn đặt hàng mới", "dragonphone17@gmail.com", "dragonphone17@gmail.com", "tgarnlbhgqedgzpt", content2);
            int cartId = db.GioHangs.Where(row => row.MaTV == tv.MaTV).Select(row => row.MaGioHang).SingleOrDefault();
            string paymentUrl = urlPayment(DaThanhToan, cartId,thanhtien);
            List<ChiTietGioHang> listCartDetail = db.ChiTietGioHangs.Where(row => row.MaGioHang == cartId).ToList();

            foreach (var item in listCartDetail)
            {
                db.ChiTietGioHangs.Remove(item);
            }

            db.SaveChanges();
            GioHang cart = db.GioHangs.Where(row => row.MaGioHang == cartId).SingleOrDefault();
            db.GioHangs.Remove(cart);
            db.SaveChanges();
            if(DaThanhToan=="Thanh Toán VNPAY")
            {
                return Redirect(paymentUrl);
            }
            else
            {
                return RedirectToAction("paymentsuccess","GioHang");
            }
        }
        public void GuiMail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(ToEmail);
                mail.From = new MailAddress(FromEmail);
                mail.Subject = Title;
                mail.Body = Content;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(FromEmail, PassWord);
                smtp.EnableSsl = true; // Sử dụng SSL

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                // Ghi log hoặc xử lý ngoại lệ theo yêu cầu của ứng dụng
                throw ex; // Hoặc xử lý ngoại lệ một cách graceful
            }
        }
        public string urlPayment(string TypePaymentVn, int magiohang, decimal tongtien)
        {
            var UrlPayment = "";
            var giohang = db.GioHangs.FirstOrDefault(n=>n.MaGioHang==magiohang);
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key
            //Save order to db

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (tongtien * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVn=="Thanh Toán VNPAY ")
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }

            vnpay.AddRequestData("vnp_CreateDate",DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + giohang.MaGioHang);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", giohang.MaGioHang.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            UrlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return UrlPayment;
        }
        public ActionResult paymentsuccess()
        {
            return View();
        }
    }
}