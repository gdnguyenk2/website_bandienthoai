using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace webbandienthoai.Controllers
{
    public class LienHeController : Controller
    {
        // GET: LienHe
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LienHe(string txtTen, string txtChuDe, string txtEmail, string txtContent)
        {
            string content = "Liên hệ từ: " + txtTen+"<br/>"+"Email: "+txtEmail + "<br/><br/>" + txtContent;
            GuiMail(txtChuDe, "dragonphone17@gmail.com", "dragonphone17@gmail.com", "tgarnlbhgqedgzpt",content);
            return RedirectToAction("Index");
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
    }
}