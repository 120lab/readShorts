using readShorts.Models.Commands;
using readShorts.Models.dbo;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace readShorts.BusinessLogic.Interfaces
{
    public interface IMatchAlgoBL : IBaseBL
    {
        IEnumerable<ShortUserAccount> GetMatch(IDictionary<long, string> shorts, MatchShortUserAccountCommand comm, Int64 user);
    }
}
