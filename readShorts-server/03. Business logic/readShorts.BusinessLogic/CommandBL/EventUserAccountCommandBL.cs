using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace readShorts.BusinessLogic.Interfaces
{
    public class EventUserAccountCommandBL : BaseBL, IEventUserAccountCommandBL
    {
        private readonly IEventUserAccountCommandRepository _EventUserAccountCommandRepository;

        public EventUserAccountCommandBL(IEventUserAccountCommandRepository EventUserAccountCommandRepository,
            IUnitOfWork unitOfWork, ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _EventUserAccountCommandRepository = EventUserAccountCommandRepository;
        }

        public EventUserAccountViewModel Add(EventUserAccountCommand command)
        {
            EventUserAccountViewModel suafvm = new EventUserAccountViewModel();

            suafvm.Messages = ValidateParam(command);

            try
            {
                lock (thisLock)
                {
                    Task.Run(() =>
                    {
                        _EventUserAccountCommandRepository.Add(new Entities.dbo.EventUserAccount()
                        {
                            UserAccountKey = command.UserAccountKey,
                            LUEventTypeKey = command.LUEventTypeKey,
                            AdditionalData = command.AdditionalData
                        });

                        base.UnitOfWork.SaveChanges();
                    });
                }
            }
            catch (Exception e)

            {
                suafvm.Messages.Union(new Models.Message[] { new Models.Message(Models.LogLevel.Error, e.Message) });
            }

            return suafvm;
        }

        private IEnumerable<Models.Message> ValidateParam(EventUserAccountCommand comm)
        {
            /// TBD

            return null;
        }
    }
}