using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace BLL
{
    public static class EmailHelper
    {
        // שולח מייל בודד - שגיאה בשליחה לא תיזרק החוצה, רק תוחזר כ-false
        public static bool SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                var host = ConfigurationManager.AppSettings["SmtpHost"];
                var port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
                var user = ConfigurationManager.AppSettings["SmtpUser"];
                var pass = ConfigurationManager.AppSettings["SmtpPassword"];

                using (var client = new SmtpClient(host, port))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(user, pass);

                    using (var mail = new MailMessage(user, toAddress, subject, body))
                    {
                        mail.IsBodyHtml = true;
                        client.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Email send failed: " + ex.Message);
                return false;
            }
        }

        public static void SendWelcomeEmail(string toAddress, string fullName)
        {
            var subject = "ברוכים הבאים ל-TCG Market";
            var body = $"<p>שלום {fullName},</p><p>ההרשמה שלך בוצעה בהצלחה. בהצלחה במסחר בקלפים!</p>";
            SendEmail(toAddress, subject, body);
        }

        public static void SendPasswordResetEmail(string toAddress, string resetLink)
        {
            var subject = "איפוס סיסמה - TCG Market";
            var body = $"<p>קיבלנו בקשה לאיפוס הסיסמה שלך.</p><p>לחץ/י על הקישור הבא כדי לבחור סיסמה חדשה (בתוקף לשעה אחת):</p><p><a href=\"{resetLink}\">{resetLink}</a></p><p>אם לא ביקשת לאפס סיסמה, אפשר להתעלם מהמייל הזה.</p>";
            SendEmail(toAddress, subject, body);
        }
    }
}