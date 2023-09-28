using System.Net.Mail;
using System.Net;

namespace WebQlyPhatTu.Helper
{
    public class SendMail
    {
        public static string send(string mailTo, string htmlTemplate, string Subject = "")
        {
            // Cấu hình thông tin máy chủ SMTP
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("tuananh20417@gmail.com", "bfpjxiquylrnuzxn"),
                EnableSsl = true
            };

            try
            {
                // Tạo đối tượng MailMessage
                var message = new MailMessage();
                message.From = new MailAddress("tuananh20417@gmail.com");
                message.To.Add(mailTo);
                message.Subject = Subject;
                message.Body = htmlTemplate;
                message.IsBodyHtml = true;
                // Gửi email
                smtpClient.Send(message);
                return "Email đã được gửi thành công.";


            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gửi email: " + ex.Message);
                return "Lỗi khi gửi email: " + ex.Message;
            }
        }
    }
}
