using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using readShorts.BusinessLogic.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Models;
using readShorts.Models.Commands;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace readShorts.BusinessLogic.Interfaces
{
    public class ShortTagCommandBL : BaseBL, IShortTagCommandBL
    {
        private readonly IShortTagCommandRepository _shortTagCommandRepository;

        public ShortTagCommandBL(IShortTagCommandRepository shortTagCommandRepository, IUnitOfWork unitOfWork,
            ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _shortTagCommandRepository = shortTagCommandRepository;
        }

        public ShortTagViewModel Create(CreateShortTagCommand command)
        {
            var obj = base.Map<Models.dbo.ShortTag, Entities.dbo.ShortTag>(command);
            ShortTagViewModel uvm = new ShortTagViewModel();

            try
            {
                lock (thisLock)
                {
                    // we are using unitOfWork but the combination of
                    // ShortRepository.Add(Short) and SaveChanges() is not atomic.
                    // multiple threads calling to the following methods might cause us to some insert operations
                    _shortTagCommandRepository.Add(obj);
                    base.UnitOfWork.SaveChanges();
                }
            }
            catch (Exception e)
            {
                uvm.Messages.Union(new Models.Message[] { new Models.Message(Models.LogLevel.Error, e.Message) });
            }

            return uvm;
        }
    }
}