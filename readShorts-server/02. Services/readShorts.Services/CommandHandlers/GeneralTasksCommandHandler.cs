using System;
using System.Threading;
using Framework.Core.Interfaces.CQRS;
using Framework.Core.Utils;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;

namespace readShorts.Services.CommandHandlers
{
    public sealed class GeneralTasksCommandHandler : CommandHandlerBase, ICommandHandler<GeneralTasksCommand, GeneralTasksViewModel>
    {
        private readonly IGeneralTasksBL _bl;

        public GeneralTasksCommandHandler(IGeneralTasksBL bl, IUserQueryBL userQueryBL) 
            : base(userQueryBL)
        {
            _bl = bl;
        }

        public GeneralTasksViewModel Execute(GeneralTasksCommand command)
        {
            return _bl.Execute(command);
        }
    }
}