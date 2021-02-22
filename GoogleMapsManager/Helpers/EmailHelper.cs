using System.Configuration;

namespace GoogleMapsManager
{
    public static class EmailHelper
    {
        public static void SendMail(string to, string subject, string body, bool isBodyHTML)
        {
            using (System.Net.Mail.MailMessage messageToSend = new System.Net.Mail.MailMessage())
            {
                System.Net.Mail.MailAddress adrFrom = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["FromEmail"]);
                messageToSend.From = adrFrom;
                messageToSend.Subject = subject;
                messageToSend.IsBodyHtml = isBodyHTML;

                if (to.Substring(to.Length - 1, 1) != ";")
                {
                    to += ";";
                }

                string[] arrTo = to.Split(';');
                foreach (string emailAddress in arrTo)
                {
                    if (emailAddress.Trim() != "")
                    {
                        messageToSend.To.Add(emailAddress);
                    }
                }

                messageToSend.Body = body;
                messageToSend.IsBodyHtml = isBodyHTML;

                using (System.Net.Mail.SmtpClient mailObj = new System.Net.Mail.SmtpClient())
                {
                    mailObj.Host = ConfigurationManager.AppSettings["SmtpServer"];
                    mailObj.UseDefaultCredentials = false;
                    mailObj.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmail"],
                                                                           ConfigurationManager.AppSettings["SendEmailPassword"]);

                    mailObj.Send(messageToSend);
                }

            }
        }
    }
}