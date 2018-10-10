using readShorts.Models.dbo;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace readShorts.BusinessLogic.Interfaces
{
    public interface IShortUserAccountQueryBL : IBaseBL
    {
        /// <summary>
        /// Returns the entered user's data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ShortUserAccountViewModel GetShortsUserAccount(ShortUserAccountQuery query);

        IEnumerable<ShortUserAccount> GetShortsUserAccount(DateTime interval);
    }
}