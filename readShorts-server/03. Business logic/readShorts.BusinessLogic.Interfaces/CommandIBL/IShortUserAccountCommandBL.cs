using readShorts.Models.Commands;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System.Collections.Generic;

namespace readShorts.BusinessLogic.Interfaces
{
    public interface IShortUserAccountCommandBL : IBaseBL
    {
        void CreateShortUserAccount(long userAccountKey, List<long> shortsList);

        /// <summary>
        /// Creates a new user with the entered data
        /// </summary>
        /// <param name="command"></param>
        ShortUserAccountViewModel CreateShortUserAccount(CreateShortUserAccountCommand command);

        /// <summary>
        /// Updates the entered user
        /// </summary>
        /// <param name="command"></param>
        ShortUserAccountViewModel UpdateShortUserAccount(UpdateShortUserAccountCommand command);

    }
}
