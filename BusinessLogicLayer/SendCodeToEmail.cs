using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using MailKit.Net.Smtp;
using MimeKit;

namespace BusinessLogicLayer
{
    public class SendCodeToEmail
    {
        public static string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
        public static string SendVerificationEmail(Officer officer)
        {
            string otpCode = GenerateOTP();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Phòng vật tư Uneti", "httung.dhti15a20hn@sv.uneti.edu.vn"));
            message.To.Add(new MailboxAddress("", officer.email));
            message.Subject = "Cảnh báo bảo mật";

            message.Body = new TextPart("plain")
            {
                Text = $"Mã OTP của bạn là: {otpCode}\nVui lòng không chia sẻ mã này cho bất kỳ ai!"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("httung.dhti15a20hn@sv.uneti.edu.vn", "khva uhku eunq zfpw");
                client.Send(message);
                client.Disconnect(true);
            }

            return otpCode;
        }
    }
}
