using readShorts.Models.Commands;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System.Collections.Generic;

namespace readShorts.BusinessLogic.Interfaces
{
    public interface IEventUserAccountCommandBL : IBaseBL
    {
        /// <summary>
        /// Updates the entered user
        /// </summary>
        /// <param name="command"></param>
        EventUserAccountViewModel Add(EventUserAccountCommand command);
    }
}