using System.Collections.Generic;
using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.Commands
{
    public class CreateShortUserAccountCommand : ICommand
    {
        public CreateShortUserAccountCommand()
        {
            ShortUserAccounts = new HashSet<ShortUserAccount>();
        }

        public ICollection<ShortUserAccount> ShortUserAccounts { get; set; }
    }
}
