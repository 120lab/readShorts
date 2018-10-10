using System.Collections.Generic;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.ViewModels
{

    public sealed class UserViewModel : BaseViewModel, IQueryResult
    {
        public UserViewModel()
        {
            Users = new List<UserAccount>();            
        }

        public IEnumerable<UserAccount> Users { get; set; }        
    }
}
