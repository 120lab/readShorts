using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using readShorts.BusinessLogic.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Models;
using readShorts.Models.Commands;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace readShorts.BusinessLogic
{
    public class UserAccountPointCommandBL : BaseBL, IUserAccountPointCommandBL
    {
        private readonly IUserQueryBL _userQueryBL;
        private readonly IUserAccountPointCommandRepository _userAccountPointCommandRepository;

        public UserAccountPointCommandBL(IUserQueryBL userQueryBL,
            IUserAccountPointCommandRepository userAccountPointCommandRepository,
            IUnitOfWork unitOfWork, ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _userQueryBL = userQueryBL;
            _userAccountPointCommandRepository = userAccountPointCommandRepository;
        }

        public UserAccountPointViewModel UpdatePoints(CreateUserAccountPointCommand command)
        {
            UserAccountPointViewModel uapvm = new UserAccountPointViewModel();

            if (!command.UserAccountKey.HasValue)
            {
                UserViewModel usr = _userQueryBL.GetUser(new UserQuery() { UserName = command.UserName });                

                if (usr != null && usr.Users != null)
                {
                    command.UserAccountKey = usr.Users.FirstOrDefault().RecordKey;
                }
                else
                {
                    uapvm.Messages.Union(usr.Messages);
                }
            }

            if (command.UserAccountKey.HasValue)
            {
                try
                {
                    lock (thisLock)
                    {
                        // we are using unitOfWork but the combination of
                        // UserRepository.Add(user) and SaveChanges() is not atomic.
                        // multiple threads calling to the following methods might cause us to some insert operations
                        int pointsValue = GetPointValue(command.LUPointTypeKey);
                        _userAccountPointCommandRepository.Add(new Entities.dbo.UserAccountPoint()
                        {
                            UserAccountKey = command.UserAccountKey.Value,
                            Value = pointsValue
                        });

                        base.UnitOfWork.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    uapvm.Messages.Union(new Message[] { new Models.Message(LogLevel.Error, e.Message) });
                }
            }

            return uapvm;
        }

        private int GetPointValue(Models.Enums.LULUPointTypeEnum lUPointTypeKey)
        {
            int val = 0;
            switch (lUPointTypeKey)
            {
                case Enums.LULUPointTypeEnum.Points:
                case Enums.LULUPointTypeEnum.VirtualCoins:
                case Enums.LULUPointTypeEnum.Reading:
                case Enums.LULUPointTypeEnum.Sharing:
                case Enums.LULUPointTypeEnum.ReadingFromSharing:
                case Enums.LULUPointTypeEnum.Feedbacking:
                case Enums.LULUPointTypeEnum.RegisteredFromInvitation:
                case Enums.LULUPointTypeEnum.AcceptedWritersFromInvitation:
                case Enums.LULUPointTypeEnum.FollowWrtier:
                case Enums.LULUPointTypeEnum.Bookmark:
                case Enums.LULUPointTypeEnum.Like:
                    val = 1;
                    break;
            }

            return val;
        }
    }
}