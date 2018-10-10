using System.Collections.Generic;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.ViewModels
{
    public sealed class UserAccountPointViewModel : BaseViewModel, IQueryResult
    {
        public UserAccountPointViewModel()
        {
            UserAccountPoint = new List<UserAccountPoint>();
        }

        public IEnumerable<UserAccountPoint> UserAccountPoint { get; set; }

    }

}
