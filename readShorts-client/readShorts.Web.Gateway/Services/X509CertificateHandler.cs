using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace readShorts.Web.Services
{
    public class X509CertificateHandler : DelegatingHandler
    {
        private ConfigurationService configuraion = new ConfigurationService();

        public X509CertificateHandler()
        {
            //this.ValidateCertificate = validateCertificate;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!HttpContext.Current.Request.ClientCertificate.IsPresent)
            {
                return Task.Factory.StartNew(() => request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            var cert = new X509Certificate2(HttpContext.Current.Request.ClientCertificate.Certificate);
            if (!this.ValidateCertificate(cert))
            {
                return Task.Factory.StartNew(() => request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            var identity = new GenericIdentity(cert.Subject, "ClientCertificate");
            var principal = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;

            return base.SendAsync(request, cancellationToken);
        }

        private bool ValidateCertificate(X509Certificate2 cert)
        {
            return cert.Thumbprint == configuraion.GetThumbprint();
        }
    }
}