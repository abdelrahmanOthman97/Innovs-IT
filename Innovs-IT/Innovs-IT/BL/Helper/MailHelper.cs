using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Innovs_IT.BL.Helper
{
    public class MailHelper
    {
        public static string sendMail(string Title, string Message, string mailTo)
        {
            try
            {
                
                SmtpClient smtp = new SmtpClient("mail.innovs-it.com", 465);

                smtp.EnableSsl = true;

                smtp.Credentials = new NetworkCredential("abdelrahman@innovs-it.com", "An@2021");

                smtp.Send("abdelrahman@innovs-it.com", mailTo, Title, Message);

                return "Mail Sent Successfully";

                //MailMessage m = new MailMessage();

                //m.From = "";
                //m.To = "";
                //m.Subject = "";
                //m.Body = "";
                //m.CC = "";
                //m.Attachments = "";
                


            }
            catch (Exception ex)
            {

                return "Mail Faild";
            }
        }
    }
}
