using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using ELib.Common;
using ELib.BL.Services.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class SendEmailService : ISendEmailService
    {
        private readonly ELogger logger;

        public SendEmailService()
        {
            logger = ELoggerFactory.GetInstance().GetLogger(GetType().FullName);
        }
        public void SendEmail(string email, string login)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("ElectronicLibraryORG@gmail.com", "QazLibrary2015");
            smtp.EnableSsl = true;
            MailMessage message = new MailMessage();
            message.From = new MailAddress("ElectronicLibraryORG@gmail.com");
            message.To.Add(new MailAddress(email));
            message.Subject = "Successful registration on ELib";
            message.Body = string.Format("Welcome Dear {0}. \n\n Kind regards, Team", login);
            try
            {
                smtp.Send(message);
            }
            catch (Exception e)
            {
                logger.Error("Error In SendSMS", e);
            }
        }
    }
}
