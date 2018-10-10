using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace readShorts.Web.Services
{
    public class ConfigurationService
    {
        public string innerHostAddressAuth()
        {
            return ConfigurationManager.AppSettings["innerHostAddressAuth"];
        }

        public string InnerHostAddress()
        {
            return ConfigurationManager.AppSettings["innerHostAddress"];
        }

        public string GetThumbprint()
        {
            return ConfigurationManager.AppSettings["Thumbprint"];
        }

        public string GetThumbprintAuth()
        {
            return ConfigurationManager.AppSettings["ThumbprintAuth"];
        }
    }
}