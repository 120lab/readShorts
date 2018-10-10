using System.Collections.Generic;
using System.Web.Http;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.Queries;
using readShorts.Models.Queries.Results;

namespace readShorts.API.External.Controllers
{
    /// <summary>
    /// Test Controller for DI using Ninject
    /// </summary>
    [RoutePrefix("api/Tasks")]
    public class TaskController : ApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        
        public TaskController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        /// <summary>
        /// List the tasks in the DB
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{query}")]
        public IHttpActionResult TaskList(TasksByStatusQuery query)
        {
            var result = _queryDispatcher.Dispatch<TasksByStatusQuery, TasksByStatusQueryResult>(query);
            return null;
        }
               

        //[HttpPost]
        //[Route("{command}")]
        //public ActionResult ChangeTaskStatus(ChangeTaskStatusCommand command)
        //{
        //    _commandDispatcher.Dispatch(command);
        //    return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        //}

        /// <summary>
        /// GET api/<controller>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}