using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.Commands
{
    public sealed class CreateAuditCommand : ICommand
    {
        public string ActionName { get; set; }

        public string UserId { get; set; }
    }
}
