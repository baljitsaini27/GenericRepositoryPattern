using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Taleem.Services
{
    public interface IEmailSender
    {
        Task SendMailAsync(string ToEmail, string Subject, string EmailBody, string CC = "", string BCC = "", List<Attachment> Attachments = null);
    }
}
