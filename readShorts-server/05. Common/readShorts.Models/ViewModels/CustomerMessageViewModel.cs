using System.Collections.Generic;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.ViewModels
{

    public sealed class CustomerMessageViewModel : BaseViewModel, IQueryResult
    {
        public CustomerMessageViewModel()
        {
            CustomerMessage = new List<CustomerMessage>();
        }

        public IEnumerable<CustomerMessage> CustomerMessage { get; set; }
    }
}
