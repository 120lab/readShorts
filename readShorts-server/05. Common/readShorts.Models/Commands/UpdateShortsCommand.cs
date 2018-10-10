using System.Collections.Generic;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.Commands
{
    public class UpdateShortsCommand : ICommand
    {
        public UpdateShortsCommand()
        {
            Shorts = new HashSet<Short>();
        }

        public ICollection<Short> Shorts { get; set; }
    }
}
