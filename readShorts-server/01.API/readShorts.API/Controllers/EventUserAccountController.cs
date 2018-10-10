using Framework.Core.Interfaces.CQRS;
using readShorts.API.Filters;
using readShorts.Models.Commands;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System.Web.Http;
using System.Web.Http.Description;

namespace readShorts.API.Internal.Controllers
{
    [RoutePrefix("api/EventUserAccount")]
    public class EventUserAccountController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public EventUserAccountController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        //[HttpGet]
        //[ResponseType(typeof(EventUserAccountViewModel))]
        ////[Route("{query}")]
        //public IHttpActionResult Get([FromUri]EventUserAccountQuery query)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = _queryDispatcher.Dispatch<EventUserAccountQuery, EventUserAccountViewModel>(query);
        //    return Ok(result);
        //}

        [HttpPost]
        [ResponseType(typeof(EventUserAccountViewModel))]
        public IHttpActionResult UpdateWriterAsFollower([FromBody]EventUserAccountCommand command)
        {
            var result = _commandDispatcher.Dispatch<EventUserAccountCommand, EventUserAccountViewModel>(command);

            return Ok(result);
        }
    }
}