using Framework.Core.Interfaces.Audit;

namespace readShorts.Services.Interfaces
{
    public interface IAuditServices : IServiceBase
    {
        void Report(IAuditMessage message);
    }
}
