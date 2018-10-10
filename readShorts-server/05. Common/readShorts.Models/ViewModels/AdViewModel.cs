using System.Collections.Generic;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.ViewModels
{

    public sealed class AdViewModel : BaseViewModel, IQueryResult
    {
        public AdViewModel()
        {
            Ads = new List<Ad>();            
        }
        
        public IEnumerable<Ad> Ads { get; set; }        
    }
}
