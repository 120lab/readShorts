using Framework.Core.Interfaces.CQRS;
using readShorts.API.Filters;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System.Web.Http;
using System.Web.Http.Description;

namespace readShorts.API.Internal.Controllers
{
    [RoutePrefix("api/Lookup")]
    public class LookupController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public LookupController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [ResponseType(typeof(LookupViewModel))]
        //[Route("{query}")]
        public IHttpActionResult Get([FromUri]LookupQuery query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _queryDispatcher.Dispatch<LookupQuery, LookupViewModel>(query);
            return Ok(result);
        }
    }
}