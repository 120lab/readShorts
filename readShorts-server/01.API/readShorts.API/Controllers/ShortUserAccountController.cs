using Framework.Core.Interfaces.CQRS;
using readShorts.API.Filters;
using readShorts.Models.Commands;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System.Web.Http;
using System.Web.Http.Description;

namespace readShorts.API.Internal.Controllers
{
    [RoutePrefix("api/ShortUserAccount")]
    public class ShortUserAccountController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ShortUserAccountController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        //[HttpGet]
        //[ResponseType(typeof(ShortUserAccountViewModel))]
        ////[Route("{query}")]
        //public IHttpActionResult Get([FromUri]ShortUserAccountQuery query)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = _queryDispatcher.Dispatch<ShortUserAccountQuery, ShortUserAccountViewModel>(query);
        //    return Ok(result);
        //}

        [HttpPost]
        [ResponseType(typeof(ShortUserAccountViewModel))]
        public IHttpActionResult UpdateShortUserAccount([FromBody]UpdateShortUserAccountCommand command)
        {
            var result = _commandDispatcher.Dispatch<UpdateShortUserAccountCommand, ShortUserAccountViewModel>(command);

            return Ok(result);
        }
    }
}