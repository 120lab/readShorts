using readShorts.Web.Models;
using readShorts.Web.Networking;
using readShorts.Web.Services;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace readShorts.Web.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(ApplicantModel applicant)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["user"] != null)
            {
                if (HttpContext.Current.Session["user"].ToString() != applicant.identity)
                {
                    return BadRequest("User Already Logged In");
                }
            }
            string token = TokenService.GenerateToken(applicant.identity, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.UserAgent, DateTime.Now.Ticks);
            INetworkHttpClient networkHttpClient = new NetworkHttpClient(API_TYPE.External, token);
            var data = networkHttpClient.ReadAsync("api/UserAccount?identity=" + applicant.identity + "&password=" + applicant.password);
            //HttpContext.Current.Session.Add("user", applicant.identity);
            HttpContext.Current.Response.AppendHeader("SessionID", token);
            HttpContext.Current.Response.AppendHeader("Access-Control-Expose-Headers", "SessionID");
            //HttpContext.Current.Session.Add("SessionID", token);

            return Ok(data);
        }

        [HttpGet]
        [Route("LogOut")]
        public IHttpActionResult LogOut()
        {
            INetworkHttpClient networkHttpClient = new NetworkHttpClient(API_TYPE.External);
            networkHttpClient.ReadAsync("api/Applicant/LogOut");
            HttpContext.Current.Session.Abandon();
            return Ok();
        }
    }
}