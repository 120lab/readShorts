using System.Collections.Generic;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.ViewModels
{
    public sealed class ShortUserAccountViewModel : BaseViewModel, IQueryResult
    {
        public ShortUserAccountViewModel()
        {
            ShortUserAccounts = new List<ShortUserAccount>();
        }

        public IEnumerable<ShortUserAccount> ShortUserAccounts { get; set; }
      
    }
   
}
