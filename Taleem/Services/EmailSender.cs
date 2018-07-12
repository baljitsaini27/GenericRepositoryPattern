using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Taleem.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        //public Task SendEmailAsync(string email, string subject, string message)
        //{
        //    return Task.CompletedTask;
        //}


        private readonly IConfiguration _Configuration;

        public EmailSender(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public async Task SendMailAsync(string ToEmail, string Subject, string EmailBody, string CC = "", string BCC = "", List<Attachment> Attachments = null)
        {
            await Task.Run(() => SendMail(ToEmail, Subject, EmailBody, CC, BCC, Attachments));
        }

        public void SendMail(string ToEmail, string Subject, string EmailBody, string CC = "", string BCC = "", List<Attachment> Attachments = null)
        { 
            bool emailIsTest = Convert.ToBoolean(Convert.ToInt32(_Configuration.GetSection("AppSettings:emailIsTest").Value.ToString()));
            string emailHost = _Configuration.GetSection("AppSettings:emailHost").Value.ToString();
            string emailUsername = _Configuration.GetSection("AppSettings:emailUsername").Value.ToString();
            string emailPassword = _Configuration.GetSection("AppSettings:emailPassword").Value.ToString();
            int emailPort = Convert.ToInt32(_Configuration.GetSection("AppSettings:emailPort").Value.ToString());
            bool emailEnableSSL = Convert.ToBoolean(Convert.ToInt32(_Configuration.GetSection("AppSettings:emailEnableSSL").Value.ToString()));
            string emailFrom = _Configuration.GetSection("AppSettings:emailFrom").Value.ToString();
            string emailDev = _Configuration.GetSection("AppSettings:emailDev").Value.ToString();

            MailMessage message = new MailMessage();
            if (emailIsTest)
            {
                string[] ToEmailAddresses = emailDev.Split(',');
                foreach (string email in ToEmailAddresses)
                {
                    MailAddress ToEmailAddress = new MailAddress(email);
                    message.To.Add(ToEmailAddress);
                }
            }
            else
            {
                string[] ToEmailAddresses = ToEmail.Split(',');

                foreach (string email in ToEmailAddresses)
                {
                    MailAddress ToEmailAddress = new MailAddress(email);
                    message.To.Add(ToEmailAddress);
                }

                if (!String.IsNullOrEmpty(CC))
                {
                    var CCAddresses = CC.Split(',');
                    foreach (var cc in CCAddresses)
                    {
                        MailAddress ToCCAddress = new MailAddress(cc);
                        message.CC.Add(ToCCAddress);
                    }
                }

                if (!String.IsNullOrEmpty(BCC))
                {
                    var BCCAddresses = BCC.Split(',');
                    foreach (var Bcc in BCCAddresses)
                    {
                        MailAddress ToBCCAddress = new MailAddress(Bcc);
                        message.Bcc.Add(ToBCCAddress);
                    }
                }
            }

            message.Body = EmailBody;//.Replace("@InstanceName", System.Configuration.ConfigurationManager.AppSettings["InstanceName"]);
            message.Subject = Subject;
            message.IsBodyHtml = true;

            message.From = new MailAddress(emailFrom);

            SmtpClient SMTPClient = new SmtpClient();
            System.Net.NetworkCredential ntwd = new NetworkCredential();

            if (Attachments != null && Attachments.Count > 0)
            {
                foreach (Attachment attachment in Attachments)
                {
                    message.Attachments.Add(attachment);
                }
            }
            try
            {
                SMTPClient.UseDefaultCredentials = true;
                ntwd.UserName = emailUsername;
                ntwd.Password = emailPassword;
                SMTPClient.Host = emailHost;
                SMTPClient.Port = emailPort;
                SMTPClient.EnableSsl = emailEnableSSL;
                SMTPClient.Credentials = ntwd;// new System.Net.NetworkCredential(emailUsername, emailPassword);
                SMTPClient.Send(message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                SMTPClient = null;
            }
        }
    }
}
