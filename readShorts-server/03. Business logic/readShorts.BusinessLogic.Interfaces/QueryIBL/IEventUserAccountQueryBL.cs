using readShorts.Models.Queries;
using readShorts.Models.ViewModels;

namespace readShorts.BusinessLogic.Interfaces
{
    public interface IEventUserAccountQueryBL : IBaseBL
    {
        /// <summary>
        /// Returns the entered user's data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        EventUserAccountViewModel GetEventUserAccount(EventUserAccountQuery query);

    }
}