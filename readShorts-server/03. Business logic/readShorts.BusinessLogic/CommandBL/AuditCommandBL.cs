using System;
using AutoMapper;
using Framework.DataAccess.Interfaces;
using readShorts.BusinessLogic.Interfaces;
using Framework.Core.Interfaces.Log;
using readShorts.DataAccess.Repositories.Interfaces.Commands;

namespace readShorts.BusinessLogic
{

    public class AuditCommandBL : BaseBL, IAuditCommandBL
    {
        [NonSerialized]
        private readonly IAuditCommandRepository _auditCommandRepository;

      

        public AuditCommandBL(IAuditCommandRepository auditRepository, IUnitOfWork unitOfWork, ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _auditCommandRepository = auditRepository;
        }

        public void Add(Models.dbo.Audit auditModel)
        {
            var audit = Mapper.Map<Models.dbo.Audit, Entities.dbo.Audit>(auditModel);

            lock (thisLock)
            {
                // we are using unitOfWork but the combination of  
                // auditRepository.Add(audit) and SaveChanges() is not atomic.
                // multiple threads calling to the following methods might cause us to some insert operations
                _auditCommandRepository.Add(audit);
                base.UnitOfWork.SaveChanges();
            }
        }
    }
}
