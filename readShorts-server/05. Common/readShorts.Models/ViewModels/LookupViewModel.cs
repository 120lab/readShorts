using Framework.Core.Interfaces.CQRS;
using readShorts.Models.LOOKUP;
using System.Collections.Generic;

namespace readShorts.Models.ViewModels
{
    public sealed class LookupViewModel : BaseViewModel, IQueryResult
    {
        public LookupViewModel()
        {
            Lookups = new List<LookupBase>();
        }

        public IEnumerable<LookupBase> Lookups { get; set; }
        public string TableName { get; set; }
    }
}