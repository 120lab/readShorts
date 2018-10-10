using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;

namespace readShorts.BusinessLogic.Interfaces
{
    public interface IAdQueryBL : IBaseBL
    {
        /// <summary>
        /// Return all users in the system
        /// </summary>
        /// <returns></returns>
        AdViewModel GetAdsFullData(Int64 lUSysInterfaceLanguageKey, int adsCount);
    }
}
