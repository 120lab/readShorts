using System;
using System.Threading;
using Framework.Core.Interfaces.CQRS;
using Framework.Core.Utils;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;

namespace readShorts.Services.CommandHandlers.Audit
{
    public sealed class CreateAuditCommandHandler : CommandHandlerBase, ICommandHandler<CreateAuditCommand,AuditViewModel>
    {
        public IAuditCommandBL AuditCommandBL { get; private set; }

        private static int _index;

        public CreateAuditCommandHandler(IAuditCommandBL auditCommandBL, IUserQueryBL userQueryBL) 
            : base(userQueryBL)
        {
            AuditCommandBL = auditCommandBL;
        }
        
        public AuditViewModel Execute(CreateAuditCommand command)
        {
            var serialized = SerializeHelper.ConvertObjectToXmlString(command);

            var audit = new Models.dbo.Audit
            {
                Id = Guid.NewGuid(),
                TypeName = command.GetType().FullName + "," + command.GetType().Assembly.GetName(),
                // ProcessId = this._processIdProvider.Id,
                Body = serialized,
                Date = DateTime.UtcNow,
                Location = Interlocked.Increment(ref _index),
                
                IsRowDeleted = false
            };
            
            AuditCommandBL.Add(audit);

            return new AuditViewModel();
        }
    }
}
