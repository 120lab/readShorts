using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using readShorts.BusinessLogic.ServiceBL;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Entities.dbo;
using readShorts.Models;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace readShorts.BusinessLogic.Interfaces
{
    public class UserCommandBL : BaseBL, IUserCommandBL
    {
        private readonly ILookupQueryBL _lookupQueryBL;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryBL _userQueryBL;
        private const int CONST_MAX_LENGTH_USER_NAME = 10;
        private const int CONST_MAX_USER_POSTFIX = 9999;

        public UserCommandBL(IUserCommandRepository userCommandRepository, IUnitOfWork unitOfWork, IUserQueryBL userQueryBL,
            ILookupQueryBL lookupQuery, ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _userCommandRepository = userCommandRepository;
            _userQueryBL = userQueryBL;
            _lookupQueryBL = lookupQuery;
        }

        public UserViewModel CreateUser(CreateUserCommand command)
        {
            var user = base.Map<Models.Commands.CreateUserCommand, Entities.dbo.UserAccount>(command); //Mapper.Map<Models.dbo.User, Entities.dbo.User>(command);
            UserViewModel uvm = new UserViewModel();

            try
            {
                if (user == null)
                {
                    ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Invalid input"));
                    return uvm;
                }

                FillDefaultValues(user, uvm);

                if (!InputValidations(user, uvm))
                {
                    return uvm;
                }

                DetectLanuguage(user, uvm);

                SuggestUserName(user, uvm);

                if (IsUserAlreadyExist(user, uvm))
                {
                    AddUniquePostfix(user, uvm);
                }

                lock (thisLock)
                {
                    // we are using unitOfWork but the combination of
                    // UserRepository.Add(user) and SaveChanges() is not atomic.
                    // multiple threads calling to the following methods might cause us to some insert operations
                    _userCommandRepository.Add(user);
                    base.UnitOfWork.SaveChanges();

                    ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Info, "User created : " + user.UserName));

                    var t = Task.Run(() => _userQueryBL.GetUser( new Models.Queries.UserQuery() { Identity = user.EmailAddress, Password = user.HashedPassword }));

                    if (user.LUUserVerificationTypeKey == (int)Models.Enums.LUUserVerificationType.VerificationPending)
                    {
                        SendVerificationMail(user, uvm);
                    }
                    else if (user.LUUserVerificationTypeKey == (int)Models.Enums.LUUserVerificationType.Verified && user.LUSubscriptionTypeKey != (int)Models.Enums.LUSubscriptionTypeEnum.WriterPremium) 
                    {
                        SendWelcomeMail(user, uvm);
                    }

                    t.Wait();
                    uvm.Users = t.Result.Users;

                }
            }
            catch (Exception e)
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, e.Message));
            }

            return uvm;
        }

        private bool SendVerificationMail(UserAccount user, UserViewModel uvm)
        {
            MailMessage msg = SendMailBL.CreateVerificationMail(user.UniqueGuid, user.LastActitiyDate.ToBinary(), user.EmailAddress, user.FirstName);

            bool result = SendMailBL.Execute(msg);
            if (result)
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Info, "Email address verification mail sent to user : " + user.UserName));
            }
            else
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Email address verification mail cancel to user : " + user.UserName));
            }

            return result;
        }

        private bool SendWelcomeMail(UserAccount user, UserViewModel uvm)
        {
            MailMessage msg = SendMailBL.CreateWelcomeMail(user.EmailAddress, user.FirstName);

            bool result = SendMailBL.Execute(msg);
            if (result)
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Info, "Welcome email sent to user : " + user.UserName));
            }
            else
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Welcome email cancel to user : " + user.UserName));
            }

            return result;
        }

        private void FillDefaultValues(UserAccount user, UserViewModel uvm)
        {
            //user.FirstName = char.ToUpper(user.FirstName[0]) + user.FirstName.ToLower().Substring(1);
            //user.LastName = char.ToUpper(user.LastName[0]) + user.LastName.ToLower().Substring(1);
            user.FirstName = user.FirstName.ToLower();
            user.LastName = user.LastName.ToLower();
            user.BirthDate = (user.BirthDate <= DateTime.MinValue) ? DateTime.UtcNow : user.BirthDate;
            //user.EmailAddress = Regex.Replace(user.EmailAddress, "^[a-zA-Z][a-zA-Z0-9]*$", "");
            //user.EmailAddress = char.ToUpper(user.EmailAddress[0]) + user.EmailAddress.ToLower().Substring(1);
            user.EmailAddress = user.EmailAddress.ToLower();
            user.FirstName = user.FirstName.ToLower();
            user.LastName = user.LastName.ToLower();
            user.HashedPassword = Framework.Core.Utils.StringExtender.GetRandomString();
            user.UniqueGuid = Framework.Core.Utils.StringExtender.GetRandomString();
            user.LastActitiyDate = DateTime.UtcNow;
            if (user.IsEmailConnect && user.LUSubscriptionTypeKey != (int)Models.Enums.LUSubscriptionTypeEnum.WriterPremium)
            {
                user.LUUserVerificationTypeKey = (int)Models.Enums.LUUserVerificationType.VerificationPending;
                user.LUGenderKey = (int)Models.Enums.LUGenderEnum.Other;
                user.LUCountryKey = (int)Models.Enums.LUCountryEnum.Other;
            }
            else if (user.IsEmailConnect && user.LUSubscriptionTypeKey == (int)Models.Enums.LUSubscriptionTypeEnum.WriterPremium)
            {
                user.LUUserVerificationTypeKey = (int)Models.Enums.LUUserVerificationType.Verified;
            }
            else if (user.IsFBConnect)
            {
                user.LUUserVerificationTypeKey = (int)Models.Enums.LUUserVerificationType.Verified;
            }
            else
            {
                user.LUUserVerificationTypeKey = (int)Models.Enums.LUUserVerificationType.Anonymous;
            }
        }

        private void DetectLanuguage(UserAccount user, UserViewModel uvm)
        {
            /// When language detected nop
            if (user.LUSysInterfacelanguageKey == (int)Models.Enums.SysInterfaceLanguageEnum.English ||
               user.LUSysInterfacelanguageKey == (int)Models.Enums.SysInterfaceLanguageEnum.Hebrew)
                return;

            /// When language empty, check if can find Israel IP else English as default
            if (!string.IsNullOrEmpty(user.ClientIP))
            {
                if (IpDetectionBL.IsIsraelIP(user.ClientIP))
                {
                    user.LUSysInterfacelanguageKey = (int)Models.Enums.SysInterfaceLanguageEnum.Hebrew;
                }
            }
            else
            {
                user.LUSysInterfacelanguageKey = (int)Models.Enums.SysInterfaceLanguageEnum.English;
            }

            ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Info, "Language detected"));
        }

        private bool InputValidations(UserAccount user, UserViewModel uvm)
        {
            bool maliValidations = MailValidations(user, uvm);

            bool subscriptionValidation = SubscriptionValidation(user, uvm);

            bool nameValidations = (user.IsEmailConnect) ? true : NameValidations(user, uvm);

            bool genderValidation = GenderValidation(user, uvm);

            bool countryValidation = CountryValidation(user, uvm);

            return (!nameValidations || !maliValidations || !subscriptionValidation || !genderValidation || !countryValidation) ? false : true;
        }

        private bool CountryValidation(UserAccount user, UserViewModel uvm)
        {
            IEnumerable<Models.LOOKUP.LookupBase> luCountries = _lookupQueryBL.Get("LUCountries", user.LUSysInterfacelanguageKey).Lookups;
            if (luCountries != null)
            {
                if (luCountries.FirstOrDefault(x => x.RecordKey == user.LUCountryKey) == null)
                {
                    ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Invalid country value"));
                    return false;
                }
            }

            return true;
        }

        private bool GenderValidation(UserAccount user, UserViewModel uvm)
        {
            IEnumerable<Models.LOOKUP.LookupBase> luGenders = _lookupQueryBL.Get("LUGenders", user.LUSysInterfacelanguageKey).Lookups;
            if (luGenders != null)
            {
                if (luGenders.FirstOrDefault(x => x.RecordKey == user.LUGenderKey) == null)
                {
                    ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Invalid gender value"));
                    return false;
                }
            }

            return true;
        }

        private bool SubscriptionValidation(UserAccount user, UserViewModel uvm)
        {
            IEnumerable<Models.LOOKUP.LookupBase> luSubscriptionTypes = _lookupQueryBL.Get("LUSubscriptionTypes", user.LUSysInterfacelanguageKey).Lookups;
            if (luSubscriptionTypes != null)
            {
                if (luSubscriptionTypes.FirstOrDefault(x => x.RecordKey == user.LUSubscriptionTypeKey) == null)
                {
                    ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Invalid subscription value"));
                    return false;
                }
            }

            return true;
        }

        private bool MailValidations(UserAccount user, UserViewModel uvm)
        {
            bool result = true;

            if (!Regex.IsMatch(user.EmailAddress, @"(@)(.+)$"))
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Email address doesn't correct"));
                result = false;
            }
            else if (IsemailAddressAlreadyExist(user, uvm))
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Email address already registered"));
                result = false;
            }

            return result;
        }

        private static bool NameValidations(UserAccount user, UserViewModel uvm)
        {
            bool result = true;

            if (string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.FirstName))
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Last and first name are a required fields"));
                result = false;
            }
            else if ((!Regex.IsMatch(user.LastName, "^[a-zA-Z][a-zA-Z0-9]*$") || !Regex.IsMatch(user.FirstName, "^[a-zA-Z][a-zA-Z0-9]*$")))
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Part of the name conatin non alfabaet"));
                result = false;
            }
            else if ((user.LastName.Length < 2 || user.FirstName.Length < 2))
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Name conatin less than 2 letters"));
                result = false;
            }

            return result;
        }

        private void SuggestUserName(Entities.dbo.UserAccount user, UserViewModel uvm)
        {
            if (!string.IsNullOrEmpty(user.FirstName) || !string.IsNullOrEmpty(user.LastName))
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Info, "Create user name from firstname and lastname"));
                user.UserName = (string.IsNullOrEmpty(user.LastName)) ? string.Format("{0}", user.FirstName) : string.Format("{0}_{1}", user.FirstName, user.LastName);
            }
            else if (!string.IsNullOrEmpty(user.EmailAddress))
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Info, "Create user name from EmailAddress"));
                user.UserName = new MailAddress(Regex.Replace(user.EmailAddress, "^[a-zA-Z][a-zA-Z0-9]*$", "")).User;
            }

            if (string.IsNullOrEmpty(user.UserName))
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Info, "Create user name from randomal string"));
                user.UserName = Framework.Core.Utils.StringExtender.GetRandomString().ToLower();
                user.UserName = Regex.Replace(user.UserName, "^[a-zA-Z][a-zA-Z0-9]*$", "");
            }

            user.UserName = "@" + user.UserName;
        }

        private void AddUniquePostfix(UserAccount user, UserViewModel uvm)
        {
            IEnumerable<UserAccount> allUsers = _userCommandRepository.GetAll();
            HashSet<string> postfixes = new HashSet<string>();

            // Is this username already exist
            var selected = (from m in allUsers
                            where m.UserName.StartsWith(user.UserName)
                            select m.UserName.Replace(user.UserName, ""));

            for (int i = 1; i < CONST_MAX_USER_POSTFIX; i++)
            {
                if (!selected.Contains(i.ToString()))
                {
                    user.UserName = string.Format("{0}{1}", user.UserName, i);
                    ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Warning, "Username postfix changed"));
                    break;
                }
            }
        }

        private bool IsUserAlreadyExist(Entities.dbo.UserAccount user, UserViewModel uvm)
        {
            IEnumerable<UserAccount> allUsers = _userCommandRepository.GetAll();

            if (allUsers != null && allUsers.Count() > 0)
            {
                string userFullMatched = (from m in allUsers
                                          where m.UserName.ToLower() == user.UserName.ToLower()
                                          select m.UserName).LastOrDefault();

                if (!string.IsNullOrEmpty(userFullMatched))
                {
                    ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Warning, "User already exist"));
                    return true;
                }
            }

            return false;
        }

        private bool IsemailAddressAlreadyExist(Entities.dbo.UserAccount user, UserViewModel uvm)
        {
            IEnumerable<UserAccount> allUsers = _userCommandRepository.GetAll();

            if (allUsers != null && allUsers.Count() > 0)
            {
                string userFullMatched = (from m in allUsers
                                          where m.EmailAddress.ToLower() == user.EmailAddress.ToLower()
                                          select m.UserName).LastOrDefault();

                if (!string.IsNullOrEmpty(userFullMatched))
                {
                    return true;
                }
            }

            return false;
        }

        public UserViewModel UpdateUser(UpdateUsersCommand command)
        {
            var user = base.Map<Models.Commands.UpdateUsersCommand, Entities.dbo.UserAccount>(command);

            UserViewModel uvm = new UserViewModel();

            try
            {
                if (!IsemailAddressAlreadyExist(user, uvm))
                {
                    ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Email address already registered"));
                    return uvm;
                }

                Entities.dbo.UserAccount foundUser = _userCommandRepository.GetAll().FirstOrDefault(x => x.EmailAddress.ToLower() == user.EmailAddress.ToLower());

                if (foundUser == null)
                {
                    ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "User not exist"));
                    return uvm;
                }

                /// If update verification this validation not relevant
                if (!command.LUUserVerificationTypeKey.HasValue && foundUser.LUUserVerificationTypeKey != (int)Models.Enums.LUUserVerificationType.Verified)
                {
                    ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "User not verified"));
                    return uvm;
                }

                lock (thisLock)
                {
                    // we are using unitOfWork but the combination of
                    // UserRepository.Add(user) and SaveChanges() is not atomic.
                    // multiple threads calling to the following methods might cause us to some insert operations
                    if (!string.IsNullOrEmpty(command.FirstName))
                        foundUser.FirstName = command.FirstName;

                    if (!string.IsNullOrEmpty(command.LastName))
                        foundUser.LastName = command.LastName;

                    if (command.BirthDate.HasValue)
                        foundUser.BirthDate = command.BirthDate.Value;

                    if (command.EmailForShortIMightLike.HasValue)
                        foundUser.EmailForShortIMightLike = command.EmailForShortIMightLike.Value;

                    if (command.EmailForShortOfTheWeek.HasValue)
                        foundUser.EmailForShortOfTheWeek = command.EmailForShortOfTheWeek.Value;

                    if (command.EmailForShortFollowingWriter.HasValue)
                        foundUser.EmailForShortFollowingWriter = command.EmailForShortFollowingWriter.Value;

                    if (command.EmailForNewSAndUpdates.HasValue)
                        foundUser.EmailForNewSAndUpdates = command.EmailForNewSAndUpdates.Value;

                    if (command.LUSubscriptionTypeKey.HasValue)
                        foundUser.LUSubscriptionTypeKey = command.LUSubscriptionTypeKey.Value;

                    if (command.LUGenderKey.HasValue)
                        foundUser.LUGenderKey = command.LUGenderKey.Value;

                    if (command.LUCountryKey.HasValue)
                        foundUser.LUCountryKey = command.LUCountryKey.Value;

                    if (command.LUSysInterfacelanguageKey.HasValue)
                        foundUser.LUSysInterfacelanguageKey = command.LUSysInterfacelanguageKey.Value;

                    bool resultSubscriptionValidation = SubscriptionValidation(foundUser, uvm);
                    bool resultGenderValidation = GenderValidation(foundUser, uvm);
                    bool resultCountryValidation = CountryValidation(foundUser, uvm);

                    if (resultSubscriptionValidation && resultGenderValidation && resultCountryValidation)
                    {
                        if (command.LUUserVerificationTypeKey.HasValue)
                        {
                            if (foundUser.LUUserVerificationTypeKey != command.LUUserVerificationTypeKey.Value &&
                                command.LUUserVerificationTypeKey.Value == (int)Models.Enums.LUUserVerificationType.Verified &&
                                foundUser.IsEmailConnect)
                            {
                                SendWelcomeMail(user, uvm);
                            }
                            foundUser.LUUserVerificationTypeKey = command.LUUserVerificationTypeKey.Value;
                        }

                        _userCommandRepository.Update(foundUser);
                        base.UnitOfWork.SaveChanges();

                        uvm = _userQueryBL.GetUser(new Models.Queries.UserQuery() { Identity = foundUser.EmailAddress });
                    }
                }
            }
            catch (Exception e)
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, e.Message));
            }

            return uvm;
        }

        /// <summary>
        /// Deletes users by the entered IDs
        /// </summary>
        /// <param name="userIds"></param>
        public UserViewModel DeleteUser(DeleteUsersCommand command)
        {
            UserViewModel uvm = new UserViewModel();

            try
            {
                lock (thisLock)
                {
                    // we are using unitOfWork but the combination of
                    // UserRepository.Add(user) and SaveChanges() is not atomic.
                    // multiple threads calling to the following methods might cause us to some insert operations

                    foreach (string item in command.Mails)
                    {
                        _userCommandRepository.Delete(x => x.EmailAddress.ToLower() == item.ToLower());
                    }
                    base.UnitOfWork.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, e.Message));
            }

            return uvm;
        }
    }
}