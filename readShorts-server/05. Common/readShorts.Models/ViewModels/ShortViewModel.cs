using System.Collections.Generic;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.ViewModels
{
    public sealed class ShortViewModel : BaseViewModel, IQueryResult
    {
        public ShortViewModel()
        {
            Shorts = new List<Short>();
        }

        public IEnumerable<Short> Shorts { get; set; }              
    }
   
}
