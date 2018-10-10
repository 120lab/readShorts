using Framework.Core.Interfaces.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace readShorts.Models.Commands
{
    public class DeleteUsersCommand : ICommand
    {
        public DeleteUsersCommand()
        {
            Mails = new HashSet<string>();
        }

        public ICollection<string> Mails { get; set; }
    }
}