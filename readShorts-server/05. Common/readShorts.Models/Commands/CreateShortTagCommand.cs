using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.Commands
{
    public sealed class CreateShortTagCommand : ShortTag, ICommand
    {
    }
}