using Framework.Core.Interfaces.CQRS;
using readShorts.API.Filters;
using readShorts.Models.Commands;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System.Web.Http;
using System.Web.Http.Description;

namespace readShorts.API.Internal.Controllers
{
    [RoutePrefix("api/GeneralTasks")]
    public class GeneralTasksController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public GeneralTasksController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        [ResponseType(typeof(GeneralTasksViewModel))]
        //[Route("{query}")]
        public IHttpActionResult Execute([FromUri]GeneralTasksQuery query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _queryDispatcher.Dispatch<GeneralTasksQuery, GeneralTasksViewModel>(query);
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(GeneralTasksViewModel))]
        //[Route("{query}")]
        public IHttpActionResult Execute([FromBody]GeneralTasksCommand query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _commandDispatcher.Dispatch<GeneralTasksCommand, GeneralTasksViewModel>(query);
            return Ok(result);
        }
    }
}