using System;
using System.Threading;
using Framework.Core.Interfaces.CQRS;
using Framework.Core.Utils;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;

namespace readShorts.Services.CommandHandlers
{
    public sealed class MatchShortUserAccountCommandHandler : CommandHandlerBase, ICommandHandler<MatchShortUserAccountCommand, MessageMatchShortUserAccountViewModel>
    {
        public IMatchShortUserAccountBL _matchShortUserAccountBL { get; private set; }

        public MatchShortUserAccountCommandHandler(IMatchShortUserAccountBL matchShortUserAccountBL, IUserQueryBL userQueryBL) 
            : base(userQueryBL)
        {
            _matchShortUserAccountBL = matchShortUserAccountBL;
        }

        public MessageMatchShortUserAccountViewModel Execute(MatchShortUserAccountCommand command)
        {
            return _matchShortUserAccountBL.MatchData(command);
        }
    }
}
