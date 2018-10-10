using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;
using System.Collections.Generic;

namespace readShorts.Models.ViewModels
{
    public sealed class ShortTagViewModel : BaseViewModel, IQueryResult
    {
        public ShortTagViewModel()
        {
            ShortTags = new List<ShortTag>();
        }

        public IEnumerable<ShortTag> ShortTags { get; set; }
    }
}