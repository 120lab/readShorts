using System.Collections.Generic;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.ViewModels
{
    public sealed class EventUserAccountViewModel : BaseViewModel, IQueryResult
    {
        public EventUserAccountViewModel()
        {
            UserEvents = new List<EventUserAccount>();
        }

        public IEnumerable<EventUserAccount> UserEvents { get; set; }
      
    }
   
}
