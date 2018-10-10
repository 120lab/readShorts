using Framework.Core.Interfaces.CQRS;
using readShorts.API.Filters;
using readShorts.Models.Commands;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System.Web.Http;
using System.Web.Http.Description;

namespace readShorts.API.Internal.Controllers
{
    [RoutePrefix("api/Short")]
    public class ShortController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ShortController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        [ResponseType(typeof(ShortViewModel))]
        //[Route("{query}")]
        public IHttpActionResult Get([FromUri]ShortQuery query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _queryDispatcher.Dispatch<ShortQuery, ShortViewModel>(query);
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(ShortViewModel))]
        public IHttpActionResult CreateShort([FromBody]CreateShortCommand command)
        {
            var result = _commandDispatcher.Dispatch<CreateShortCommand, ShortViewModel>(command);

            return Ok(result);
        }

        [HttpPut]
        [ResponseType(typeof(ShortViewModel))]
        public IHttpActionResult UpdateShort([FromBody]UpdateShortsCommand command)
        {
            var result = _commandDispatcher.Dispatch<UpdateShortsCommand, ShortViewModel>(command);

            return Ok(result);
        }
    }
}