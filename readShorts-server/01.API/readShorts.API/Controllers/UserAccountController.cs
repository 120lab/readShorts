using Framework.Core.Interfaces.CQRS;
using Newtonsoft.Json.Linq;
using readShorts.API.Filters;
using readShorts.Models.Commands;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;

namespace readShorts.API.Internal.Controllers
{
    [RoutePrefix("api/UserAccount")]
    public class UserAccountController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public UserAccountController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public IHttpActionResult Get(string identity, string password)
        {
            var result = _queryDispatcher.Dispatch<UserQuery, UserViewModel>(new UserQuery() { Identity = identity, Password = password });

            if (result != null && Models.Message.IsErrorOccured(result.Messages))
            {
                /// Fail
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(string guid, long exp)
        {
            var result = _queryDispatcher.Dispatch<UserQuery, UserViewModel>(new UserQuery() { Guid = guid });
            string html = string.Empty;
            string mail = string.Empty;
            DateTime? act = null;

            if (result != null && Models.Message.IsErrorOccured(result.Messages))
            {
                /// Fail
                html = @"<html><body><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'><img src='http://readshortsstoragedev.blob.core.windows.net/siteimages/Logo.png' alt='interactive connection' width='45' /></h1><h1 style='color: #5e9ca0; text-align: center;'>Your verification request failed</h1><h2 style='color: #2e6c80; text-align: center;'><a href='http://www.readsohrts.com'>www.readsohrts.com</a></h2></body></html>";
            }
            else
            {
                foreach (Models.dbo.UserAccount item in result.Users)
                {
                    mail = item.EmailAddress;
                    act = item.LastActitiyDate;
                }

                if (DateTime.UtcNow.ToBinary() >= exp && DateTime.UtcNow <= DateTime.FromBinary(exp).AddHours(24))
                {
                    var update = _commandDispatcher.Dispatch<UpdateUsersCommand, UserViewModel>(new UpdateUsersCommand() { EmailAddress = mail, LUUserVerificationTypeKey = (int)Models.Enums.LUUserVerificationType.Verified });

                    if (!Models.Message.IsErrorOccured(update.Messages))
                    {
                        // Success
                        html = @"<html><body><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'><img src='http://readshortsstoragedev.blob.core.windows.net/siteimages/Logo.png' alt='interactive connection' width='45' /></h1><h1 style='color: #5e9ca0; text-align: center;'>Your verification request succeeded</h1><h2 style='color: #2e6c80; text-align: center;'><a href='http://www.readshorts.com'>www.readshorts.com</a></h2></body></html>";
                    }
                    else
                    {
                        /// Fail
                        html = @"<html><body><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'><img src='http://readshortsstoragedev.blob.core.windows.net/siteimages/Logo.png' alt='interactive connection' width='45' /></h1><h1 style='color: #5e9ca0; text-align: center;'>Your verification request failed</h1><h2 style='color: #2e6c80; text-align: center;'><a href='http://www.readshorts.com'>www.readshorts.com</a></h2></body></html>";
                    }
                }
                else
                {
                    // Expired
                    html = @"<html><body><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'>&nbsp;</h1><h1 style='color: #5e9ca0; text-align: center;'><img src='http://readshortsstoragedev.blob.core.windows.net/siteimages/Logo.png' alt='interactive connection' width='45' /></h1><h1 style='color: #5e9ca0; text-align: center;'>Your verification request expired</h1><h2 style='color: #2e6c80; text-align: center;'><a href='http://www.readshorts.com'>www.readshorts.com</a></h2></body></html>";
                }
            }

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(html);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] string identity)
        {
            var result = _queryDispatcher.Dispatch<UserQuery, UserViewModel>(new UserQuery() { Identity = identity });

            if (Models.Message.IsErrorOccured(result.Messages))
            {
                return BadRequest();
            }
            else
            {
                string pass = string.Empty;
                foreach (Models.dbo.UserAccount item in result.Users)
                {
                    pass = item.HashedPassword;
                    break;
                }

                JToken resultToken = JToken.Parse("{\"token\":\"" + pass + "\"}");
                return Ok(resultToken);
            }
        }

        [HttpPost]
        [ResponseType(typeof(UserViewModel))]
        public IHttpActionResult CreateUser([FromBody]CreateUserCommand command)
        {
            var result = _commandDispatcher.Dispatch<CreateUserCommand, UserViewModel>(command);

            return Ok(result);
        }

        [HttpPut]
        [ResponseType(typeof(UserViewModel))]
        public IHttpActionResult UpdateUser([FromBody]UpdateUsersCommand command)
        {
            var result = _commandDispatcher.Dispatch<UpdateUsersCommand, UserViewModel>(command);

            if (!Models.Message.IsErrorOccured(result.Messages))
            {
                result = _queryDispatcher.Dispatch<UserQuery, UserViewModel>(new UserQuery() { Identity = command.EmailAddress });
            }

            return Ok(result);
        }

        [HttpDelete]
        [ResponseType(typeof(UserViewModel))]
        public IHttpActionResult DeleteUser([FromBody]DeleteUsersCommand command)
        {
            var result = _commandDispatcher.Dispatch<DeleteUsersCommand, UserViewModel>(command);

            return Ok(result);
        }
    }
}