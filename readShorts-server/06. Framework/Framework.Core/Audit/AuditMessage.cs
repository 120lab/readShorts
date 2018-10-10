using Framework.Core.Interfaces.Audit;

namespace Framework.Core.Audit
{
    public class AuditMessage : IAuditMessage
    {
        public string ActionName { get; set; }

        public string UserName { get; set; }
    }
}
