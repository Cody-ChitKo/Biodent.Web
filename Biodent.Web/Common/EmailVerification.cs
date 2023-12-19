using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Biodent.Web.Common
{
    public class EmailVerification
    {
        public static void SendVerificationLink(string UserEmail, string verificationLink)
        {

            var head = "<h5>Register in Biodent Myanmar</h5>";

            var content = "<p>Hello Dear Customer.<br/> Thank you very much for registering. Our Biodent Myanmar Admin Team will respond to your registration shortly.</b> </p><br/><br/><br/>";
            var footer = "<br/><p>Sincerely,<br/> Biodent Myanmar </p>";
            string subject = "Thank you for registeration.";
            string bodyContent = head + content + footer;

            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(UserEmail));  // replace with valid value 
                //message.From = new MailAddress("NetfixTechnology@office-365.work", "Biodent Myanmar", System.Text.Encoding.UTF8);
                message.From = new MailAddress("biodent@netfix.info", "Biodent Myanmar", System.Text.Encoding.UTF8);

                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = bodyContent;

                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "biodent@netfix.info",
                        Password = "VMware1!"  // replace with valid value

                    };
                    //smtp.Timeout = 200000;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    //smtp.Host = "smtp.office365.com";
                    //smtp.Host = "smtp-relay.gmail.com";
                    smtp.Host = "win03-mailpro.zth.netdesignhost.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = false;
                    
                    smtp.Send(message);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            

        }

        public static void SendApprovedLink(string UserEmail, string verificationLink)
        {

            var head = "<h5>Register in Biodent Myanmar</h5>";
            string BiodentLink = "http:\\www.biodent-myanmar.com";
            var content = "<p>Hello Dear Customer.<br/> Your account has been registered successfully. <br/>Click here to login > > <br/>" + "<p>http:\\www.biodent-myanmar.com</p>" +
                "</p><br/><br/>";
            var footer = "<br/><p>Sincerely,<br/> Biodent Myanmar </p> <br/><br/><br/> <a href="+BiodentLink+">Biodent-Myanmar!</a>";
            string subject = "Thank you for registeration.";
            string bodyContent = head + content + footer;

            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(UserEmail));  // replace with valid value 
                //message.From = new MailAddress("NetfixTechnology@office-365.work", "Biodent Myanmar", System.Text.Encoding.UTF8);
                message.From = new MailAddress("biodent@netfix.info", "Biodent Myanmar", System.Text.Encoding.UTF8);

                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = bodyContent;

                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "biodent@netfix.info",
                        Password = "VMware1!"  // replace with valid value

                    };
                    //smtp.Timeout = 200000;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    //smtp.Host = "smtp.office365.com";
                    //smtp.Host = "smtp-relay.gmail.com";
                    smtp.Host = "win03-mailpro.zth.netdesignhost.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = false;

                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}