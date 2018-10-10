using Framework.Core.Interfaces.Utils;
using System.Configuration;

namespace readShorts.API.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string HostAddress()
        {
            return ConfigurationManager.AppSettings["innerHostAddress"];
        }

        public string GetFromAddress()
        {
            return ConfigurationManager.AppSettings["fromAddress"];
        }

        public string GetToAddress()
        {
            return ConfigurationManager.AppSettings["toAddress"];
        }

        public string EmailUserName()
        {
            return ConfigurationManager.AppSettings["emailUserName"];
        }

        public string GetPassword()
        {
            return ConfigurationManager.AppSettings["password"];
        }

        public string GetSMTP()
        {
            return ConfigurationManager.AppSettings["smtp"];
        }

        public string GetSharePicturePathUrl()
        {
            return ConfigurationManager.AppSettings["SharePicturePathUrl"];
        }

        public string GetEmailAddressVerificationUrl()
        {
            return ConfigurationManager.AppSettings["EmailAddressVerificationUrl"];
        }
    }
}