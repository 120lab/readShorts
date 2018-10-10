using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace readShorts.Models.Commands
{
    public class DeleteShortsCommand
    {
        public DeleteShortsCommand()
        {
            Ids = new HashSet<Int64>();
        }

        public ICollection<Int64> Ids { get; set; }
    }
}
