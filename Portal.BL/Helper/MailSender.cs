using Portal.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BL.Helper
{
    public static class MailSender
    {


        public static string SendMail(MailVM model)
        {
            var smtpClient = new SmtpClient("smtp.office365.com",587);

            smtpClient.UseDefaultCredentials = false;

            smtpClient.EnableSsl = true;

            var networkCredential = new NetworkCredential("FinallyFRYMA1234@outlook.com", "hhfjkkM@6788");

            smtpClient.Send("FinallyFRYMA1234@outlook.com", model.Email, model.Title, model.Message);

            return "Mail Send";
        }
    }
}
