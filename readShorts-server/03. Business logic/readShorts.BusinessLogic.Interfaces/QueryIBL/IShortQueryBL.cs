using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System.Collections.Generic;


namespace readShorts.BusinessLogic.Interfaces
{
    public interface IShortQueryBL : IBaseBL
    {
        ShortViewModel GetShorts(ShortQuery query);
        IDictionary<long, string> GetShortsJsonData();
    }
}
