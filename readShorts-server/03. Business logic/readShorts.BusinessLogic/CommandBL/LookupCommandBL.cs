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
    public class LookupCommandBL : BaseBL, ILookupCommandBL
    {
        private readonly ILookupCommandRepository _lookupCommandRepository;

        public LookupCommandBL(ILookupCommandRepository lookupCommandRepository, IUnitOfWork unitOfWork,
            ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _lookupCommandRepository = lookupCommandRepository;
        }

        public LookupViewModel Create(CreateLookupCommand command)
        {
            var obj = base.Map<Models.LOOKUP.LookupBase, Entities.LOOKUP.LookupBase>(command.Obj);
            LookupViewModel uvm = new LookupViewModel();

            try
            {
                lock (thisLock)
                {
                    // we are using unitOfWork but the combination of
                    // ShortRepository.Add(Short) and SaveChanges() is not atomic.
                    // multiple threads calling to the following methods might cause us to some insert operations
                    object[] param = new object[1] { obj };
                    _lookupCommandRepository.GetType().GetMethod(string.Format("Add{0}", command.TableName)).Invoke(_lookupCommandRepository, param);

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