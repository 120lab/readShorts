using Framework.Core.Interfaces.CQRS;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;

namespace readShorts.Services.QueryHandlers
{
    public sealed class GeneralTasksQueryHandler : ServiceBase, IQueryHandler<GeneralTasksQuery, GeneralTasksViewModel>
    {
        private readonly IGeneralTasksBL _bl;

        public GeneralTasksQueryHandler(IGeneralTasksBL bl)
        {
            _bl = bl;
        }

        public GeneralTasksViewModel Retrieve(GeneralTasksQuery query)
        {
            return _bl.Execute(query);
        }
    }
}