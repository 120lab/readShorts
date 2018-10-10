using System;
using System.Threading;
using Framework.Core.Interfaces.Audit;
using Framework.Core.Utils;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Services.Interfaces;

namespace readShorts.Services
{
    public class AuditServices : ServiceBase, IAuditServices
    {
        private static int _index;

        // [NonSerialized]
        private readonly IAuditCommandBL auditBL;

        public AuditServices(IAuditCommandBL auditBL)
        {
            this.auditBL = auditBL;
        }

        public void Report(IAuditMessage message)
        {
            var serialized = SerializeHelper.ConvertObjectToXmlString(message);
            var audit = new Models.dbo.Audit
            {
                Id = Guid.NewGuid(),
                TypeName = message.GetType().FullName + "," + message.GetType().Assembly.GetName(),
                // ProcessId = this._processIdProvider.Id,
                Body = serialized,
                Date = DateTime.UtcNow,
                Location = Interlocked.Increment(ref _index),
               
                IsRowDeleted = false
            };

            auditBL.Add(audit);
        }
    }
}
