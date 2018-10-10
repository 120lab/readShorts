using System.Web.Http;
using System.Web.Http.Description;
using Framework.Core.Interfaces.CQRS;
using readShorts.API.Common.Controllers;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;

namespace readShorts.API.External.Controllers
{
    /// <summary>
    /// Projects Controller for DI using Ninject
    /// </summary>
    [RoutePrefix("api/Projects")]
    public class ProjectsController : BaseController
    {

        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        /// <summary>
        /// Constructor for injecting the Command and Query dispatchers
        /// </summary>
        /// <param name="queryDispatcher"></param>
        /// <param name="commandDispatcher"></param>
        public ProjectsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }


        //[ResponseType(typeof(ProjectResult))]
        //public async Task<IHttpActionResult> PostBook(ProjectQuery book)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    db.Books.Add(book);
        //    await db.SaveChangesAsync();

        //    // New code:
        //    // Load author name
        //    db.Entry(book).Reference(x => x.Author).Load();

        //    var dto = new BookDTO()
        //    {
        //        Id = book.Id,
        //        Title = book.Title,
        //        AuthorName = book.Author.Name
        //    };

        //    return CreatedAtRoute("DefaultApi", new { id = book.Id }, dto);
        //}

        [HttpGet]
        [ResponseType(typeof(ProjectViewModel))]
        //[Route("{query}")]
        public IHttpActionResult Get([FromUri]ProjectQuery query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _queryDispatcher.Dispatch<ProjectQuery, ProjectViewModel>(query);
            return Ok(result);
        }

        /// <summary>
        /// Returns all the projects for the entered parameters
        /// </summary>
        /// <param name="cityCode"></param>
        /// <param name="projectStatus"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("{query}")]
        //public IHttpActionResult GetProjects(int cityCode, int projectStatus)
        //{
        //    ProjectQuery query = new ProjectQuery() { CityCode = cityCode, ProjectStatus = projectStatus };

        //    var result = _queryDispatcher.Dispatch<ProjectQuery, ProjectResult>(query);
        //    return null;
        //}
        
    }
}