using SendGrid;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace readShorts.BusinessLogic.ServiceBL
{
    public static class SendMailBL
    {
        public static MailMessage CreateVerificationMail(string uniqueGuid, long binaryDateTimeUtc, string emailAddress, string name)
        {
            string verPath = string.Format(@"http://readShortsGatewayAppInt.azurewebsites.net/api/Content?controller=UserAccount&param=guid%3D{0}%26exp%3D{1}", uniqueGuid, binaryDateTimeUtc);
            MailMessage msg = new MailMessage();
            msg.To.Add(emailAddress);
            msg.Subject = "Please confirm your email @ ReadShorts.com";
            msg.Body = string.Format("<html><body><h3 style='color: #5e9ca0; text-align: center;'><img src='http://readshortsstoragedev.blob.core.windows.net/siteimages/Logo.png' alt='interactive connection' width='45' /></h3><p>&nbsp;</p><h3 style='color: #5e9ca0; text-align: left;'>Hi {0},</h3><h3 style='color: #5e9ca0; text-align: left;'>Welcome to Shorts, Let's confirm your email address.</h3><h3 style='color: #5e9ca0; text-align: left;'>By clicking on the following link in the next 24 hours, you are confirming your email address and agreeing to ReadShorts's terms</h3><p></p><h3 style='color: #5e9ca0; text-align: center;'><a href='{1}'><img src='http://readshortsstoragedev.blob.core.windows.net/siteimages/confimemail.png'></a></h3><h3 style='color: #5e9ca0; text-align: center;'>Don't worry, with Shorts you don't need a password, How cool is that, ah?</h3></body></html>", name, verPath);
            return msg;
        }

        public static MailMessage CreateWelcomeMail(string emailAddress, string name)
        {
            string verPath = string.Format("http://www.readshorts.com");
            MailMessage msg = new MailMessage();
            msg.To.Add(emailAddress);
            msg.Subject = string.Format("{0}, Welcome @ ReadShorts.com", name);
            msg.Body = string.Format("<html><body><h3 style='color: #5e9ca0; text-align: center;'><img src='http://readshortsstoragedev.blob.core.windows.net/siteimages/Logo.png' alt='interactive connection' width='45' /></h3><p>&nbsp;</p><h3 style='color: #5e9ca0; text-align: left;'>Hi {0}, Welcome to Read Shorts!</h3><h3 style='color: #5e9ca0; text-align: left;'>Thanks for signing up, it's great to have you as a part of our community!</h3><p>&nbsp;</p><h3 style='color: #5e9ca0; text-align: center;'><a href='{1}'>Get started now</a></h3></body></html>", name, verPath);
            return msg;
        }

        public static MailMessage CreateContactMail( string message)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add("contact@readshorts.com");
            msg.Subject = string.Format("Contact message from readShorts app");
            msg.Body = message;
            return msg;
        }

        public static MailMessage CreateImageForTextMail(string url,string message)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add("editor@readshorts.com");
            msg.To.Add("idan@readshorts.com");
            msg.Subject = "Shorts' Quotes picture";
            msg.Body = string.Format("{0}{1}{2}",message, Environment.NewLine, url);
            return msg;
        }

        public static bool Execute(MailMessage msg)
        {
            var myMessage = new SendGridMessage();
            // Add the message properties.
            myMessage.From = new MailAddress("ReadShorts@readshorts.com");

            foreach (MailAddress item in msg.To)
            {
                myMessage.AddTo(item.ToString());
            }

            myMessage.Subject = msg.Subject;

            //Add the HTML and Text bodies
            myMessage.Html = msg.Body;

            var credentials = new NetworkCredential("azure_fe779a43ae437a23efd32968f8e10a92@azure.com", "[password]");
            //var credentials = new NetworkCredential("azure_a0d37e0f5a4cffe706c1fd4a0c3e079d@azure.com", "[password]");
            
             var transportWeb = new Web(credentials);
            try
            {
                transportWeb.DeliverAsync(myMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}