using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace readShorts.BusinessLogic.Interfaces
{
    public class ShortUserAccountCommandBL : BaseBL, IShortUserAccountCommandBL
    {
        private const string CONST_SHORTUSERACCOUNT_NOT_FOUND_FOR_UPDATE = "ShortUserAccount not found for update";
        private readonly IShortUserAccountCommandRepository _shortUserAccountCommandRepository;
        private readonly IShortUserAccountQueryRepository _shortUserAccountQueryRepository;
        private readonly IShortQueryBL _shortQueryBL;
        private readonly IUserAccountPointCommandBL _userAccountPointBL;

        public ShortUserAccountCommandBL(IShortUserAccountCommandRepository shortUserAccountCommandRepository,
            IShortUserAccountQueryRepository shortUserAccountQueryRepository, IShortQueryBL shortQueryBL,
            IUserAccountPointCommandBL userAccountPointBL,
            IUnitOfWork unitOfWork, ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _shortUserAccountCommandRepository = shortUserAccountCommandRepository;
            _shortUserAccountQueryRepository = shortUserAccountQueryRepository;
            _shortQueryBL = shortQueryBL;
            _userAccountPointBL = userAccountPointBL;
        }

        public void CreateShortUserAccount(long userAccountKey, List<long> shortsList)
        {
            try
            {
                lock (thisLock)
                {
                    // we are using unitOfWork but the combination of
                    // UserRepository.Add(user) and SaveChanges() is not atomic.
                    // multiple threads calling to the following methods might cause us to some insert operations
                    foreach (long item in shortsList)
                    {
                        _shortUserAccountCommandRepository.Add(
                            new Entities.dbo.ShortUserAccount()
                            {
                                ShortKey = item,
                                UserAccountKey = userAccountKey,
                                ShortSendToUser = true,
                                ShortSendToUserTimeStamp = DateTime.UtcNow
                            }
                        );
                    }
                    base.UnitOfWork.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public ShortUserAccountViewModel CreateShortUserAccount(CreateShortUserAccountCommand command)
        {
            var sua = base.Map<ICollection<Models.dbo.ShortUserAccount>, ICollection<Entities.dbo.ShortUserAccount>>(command.ShortUserAccounts); //Mapper.Map<Models.dbo.User, Entities.dbo.User>(command);

            try
            {
                lock (thisLock)
                {
                    // we are using unitOfWork but the combination of
                    // UserRepository.Add(user) and SaveChanges() is not atomic.
                    // multiple threads calling to the following methods might cause us to some insert operations
                    foreach (var item in sua)
                    {
                        _shortUserAccountCommandRepository.Add(item);
                    }
                    base.UnitOfWork.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

            return null;
        }

        public ShortUserAccountViewModel UpdateShortUserAccount(UpdateShortUserAccountCommand command)
        {
            List<Models.Message> msgs = new List<Models.Message>();
            ShortUserAccountViewModel suavm = new ShortUserAccountViewModel();
            Entities.dbo.ShortUserAccount sua = _shortUserAccountQueryRepository.GetShortUserAccount(command.UserAccountKey, command.ShortKey);

            try
            {
                if (sua == null)
                {
                    msgs.Add(new Models.Message(Models.LogLevel.Error, string.Format(CONST_SHORTUSERACCOUNT_NOT_FOUND_FOR_UPDATE)));
                }
                else
                {
                    MarkShortSendToUser(sua, command.ShortSendToUser);
                    MarkShortViewByUser(sua, command.ShortViewByUser);
                    MarkShortReadByUser(sua, command.ShortReadByUserTimeInMiliSeconds);
                    MarkShortSignAsLike(sua, command.ShortSignAsLike);
                    MarkShortSignAsBookmark(sua, command.ShortSignAsBookmark);
                    MarkUserSignNextShort(sua, command.UserSignNextShort);
                    MarkShortSignWriterAsFollowed(sua, command.UserSignWriterAsFollowed);
                }
            }
            catch (Exception e)
            {
                msgs.Add(new Models.Message(Models.LogLevel.Error, string.Format(e.ToString())));
            }
            finally
            {
                suavm.Messages = msgs;
            }

            return suavm;
        }

        private void MarkShortSendToUser(Entities.dbo.ShortUserAccount sua, bool? sign)
        {
            if (sign.HasValue && sua != null && !sua.ShortSendToUser && sua.ShortSendToUser != sign)
            {
                sua.ShortSendToUser = sign.Value;
                sua.ShortSendToUserTimeStamp = DateTime.UtcNow;

                lock (thisLock)
                {
                    _shortUserAccountCommandRepository.Update(sua);
                    base.UnitOfWork.SaveChanges();
                }
            }
        }

        private void MarkShortViewByUser(Entities.dbo.ShortUserAccount sua, bool? sign)
        {
            if (sign.HasValue && sua != null && !sua.ShortViewByUser && sua.ShortViewByUser != sign)
            {
                sua.ShortViewByUser = sign.Value;
                sua.ShortViewByUserTimeStamp = DateTime.UtcNow;

                lock (thisLock)
                {
                    _shortUserAccountCommandRepository.Update(sua);
                    base.UnitOfWork.SaveChanges();
                }
            }
        }

        private void MarkShortReadByUser(Entities.dbo.ShortUserAccount sua, int shortReadByUserTimeInMiliSeconds)
        {
            if (sua != null && shortReadByUserTimeInMiliSeconds != 0)
            {
                sua.ShortReadByUserTimeInMiliSeconds = shortReadByUserTimeInMiliSeconds;
                sua.ShortReadByUserTimeStamp = DateTime.UtcNow;

                ShortViewModel svm = _shortQueryBL.GetShorts(new Models.Queries.ShortQuery() { RecordKey = sua.ShortKey, EnrichData = false });
                if (svm != null && svm.Shorts != null)
                {
                    if (shortReadByUserTimeInMiliSeconds > System.Linq.Enumerable.FirstOrDefault(svm.Shorts).ERTInMiliSeconds)
                    {
                        sua.ShortReadByUser = true;

                        lock (thisLock)
                        {
                            _shortUserAccountCommandRepository.Update(sua);
                            base.UnitOfWork.SaveChanges();
                        }
                    }
                }
            }
        }

        private void MarkShortSignAsLike(Entities.dbo.ShortUserAccount sua, bool? sign)
        {
            if (sign.HasValue && sua != null && sua.ShortSignAsLike != sign)
            {
                sua.ShortSignAsLike = sign.Value;
                sua.ShortSignAsLikeTimeStamp = DateTime.UtcNow;

                lock (thisLock)
                {
                    _shortUserAccountCommandRepository.Update(sua);
                    base.UnitOfWork.SaveChanges();
                }
            }
        }

        private void MarkShortSignAsBookmark(Entities.dbo.ShortUserAccount sua, bool? sign)
        {
            if (sign.HasValue && sua != null && sua.ShortSignAsBookmark != sign)
            {
                sua.ShortSignAsBookmark = sign.Value;
                sua.ShortSignAsBookmarkTimeStamp = DateTime.UtcNow;

                lock (thisLock)
                {
                    _shortUserAccountCommandRepository.Update(sua);
                    base.UnitOfWork.SaveChanges();
                }
            }
        }

        private void MarkUserSignNextShort(Entities.dbo.ShortUserAccount sua, bool? sign)
        {
            if (sign.HasValue && sua != null && !sua.UserSignNextShort && sua.UserSignNextShort != sign)
            {
                sua.UserSignNextShort = sign.Value;
                sua.UserSignNextShortTimeStamp = DateTime.UtcNow;

                lock (thisLock)
                {
                    _shortUserAccountCommandRepository.Update(sua);
                    base.UnitOfWork.SaveChanges();
                }
            }
        }

        private void MarkShortSignWriterAsFollowed(Entities.dbo.ShortUserAccount sua, bool? sign)
        {
            if (sign.HasValue && sua != null && sua.UserSignWriterAsFollowed != sign)
            {
                IEnumerable<Entities.dbo.ShortUserAccount> result = _shortUserAccountQueryRepository.GetShortUserAccountsSimilarWriter(sua.UserAccountKey, sua.ShortKey);

                lock (thisLock)
                {
                    foreach (Entities.dbo.ShortUserAccount item in result)
                    {
                        item.UserSignWriterAsFollowed = sign.Value;
                        item.UserSignWriterAsFollowedTimeStamp = DateTime.UtcNow;
                        _shortUserAccountCommandRepository.Update(item);
                    }

                    base.UnitOfWork.SaveChanges();
                }
            }
        }
    }
}