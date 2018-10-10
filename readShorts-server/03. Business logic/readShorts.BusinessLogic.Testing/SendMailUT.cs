using Microsoft.VisualStudio.TestTools.UnitTesting;
using readShorts.BusinessLogic.ServiceBL;
using System.Net.Mail;

namespace UnitTestServices
{
    [TestClass]
    public class SendMailUT
    {
        [TestMethod]
        public void TestSendMail_ValidAddress()
        {
            MailMessage msg = new MailMessage();
            msg.To.Add("idan.gvili@gmail.com");
            msg.Subject = "Mail verification message";
            msg.Body = "<p>Mail verification message</p><p>Please click on the link below to verify your mail address</p><p><a href='www.readshorts.com'>readshorts</a></p>";
            SendMailBL.Execute(msg);
        }
    }
}