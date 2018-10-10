using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;
using readShorts.Models.LOOKUP;

namespace readShorts.Models.Commands
{
    public sealed class CreateLookupCommand : ICommand
    {
        public LookupBase Obj { get; set; }
        public string TableName { get; set; }
    }
}