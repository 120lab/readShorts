using Framework.Core.Interfaces.CQRS;
using readShorts.API.Filters;
using readShorts.Models.Commands;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System.Web.Http;
using System.Web.Http.Description;

namespace readShorts.API.Internal.Controllers
{
    [RoutePrefix("api/MatchShortUserAccount")]
    public class MatchShortUserAccountController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public MatchShortUserAccountController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        [ResponseType(typeof(MessageMatchShortUserAccountViewModel))]
        public IHttpActionResult MatchData([FromBody]MatchShortUserAccountCommand command)
        {
            var result = _commandDispatcher.Dispatch<MatchShortUserAccountCommand, MessageMatchShortUserAccountViewModel>(command);

            return Ok(result);
        }
    }
}