using System.Web.Http;
using Framework.Core.Interfaces.CQRS;
using readShorts.API.Common.Controllers;
using readShorts.Models.Commands;

namespace readShorts.API.External.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public UsersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public void CreateUser([FromBody]CreateUserCommand command)
        {
            _commandDispatcher.Dispatch(command);
        }

        //[HttpPost]
        //public void UpdateUser(UpdateUsersCommand command)
        //{
        //    _commandDispatcher.Dispatch(command);
        //}


        //[HttpGet]
        //[ResponseType(typeof(UserViewModel))]
        //public IHttpActionResult Get([FromUri]UserQuery query)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = _queryDispatcher.Dispatch<UserQuery, UserViewModel>(query);
        //    return Ok(result);
        //}

    }
}