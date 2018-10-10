using System;
using System.Linq;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using readShorts.Models.Commands;
using readShorts.Models;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using System.Collections.Generic;
using readShorts.DataAccess.Interfaces;
using readShorts.BusinessLogic.ServiceBL;
using Newtonsoft.Json;
using Framework.Core.Utils;
using System.Threading.Tasks;

namespace readShorts.BusinessLogic.Interfaces
{
    public class ShortCommandBL : BaseBL, IShortCommandBL
    {
        private readonly IShortQueryBL _shortQueryBL;

        private readonly IShortCommandRepository _shortCommandRepository;
        private readonly IUserQueryBL _userq;

        private readonly IShortTagCommandBL _shortTagCommand;

        private readonly ILookupQueryBL _lookupQueryBL;
        private readonly ILookupCommandBL _lookupComBL;


        public ShortCommandBL(IShortCommandRepository shortCommandRepository, ILookupQueryBL lookupQueryBL,
            IShortTagCommandRepository shortTagRep, IUnitOfWork unitOfWork, ILoggerService loggerService,
            ILookupCommandBL lookupComBL, IUserQueryBL userq, IShortQueryBL shortQueryBL)
            : base(unitOfWork, loggerService)
        {
            _shortCommandRepository = shortCommandRepository;
            _userq = userq;
            _shortTagCommand = new ShortTagCommandBL(shortTagRep, unitOfWork, loggerService);
            _lookupQueryBL = lookupQueryBL;
            _lookupComBL = lookupComBL;
            _shortQueryBL = shortQueryBL;
        }

        public ShortViewModel CreateShort(CreateShortCommand command)
        {
            ShortViewModel uvm = new ShortViewModel();

            try
            {
                lock (thisLock)
                {
                    // we are using unitOfWork but the combination of  
                    // ShortRepository.Add(Short) and SaveChanges() is not atomic.
                    // multiple threads calling to the following methods might cause us to some insert operations
                    Transfer(command, uvm);
                }
            }
            catch (Exception e)
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, e.Message));
            }

            return uvm;
        }

        public ShortViewModel UpdateShort(UpdateShortsCommand command)
        {
            var objs = base.Map<ICollection<Models.dbo.Short>, ICollection<Entities.dbo.Short>>(command.Shorts);

            ShortViewModel uvm = new ShortViewModel();

            try
            {
                lock (thisLock)
                {
                    // we are using unitOfWork but the combination of  
                    // ShortRepository.Add(Short) and SaveChanges() is not atomic.
                    // multiple threads calling to the following methods might cause us to some insert operations
                    foreach (var obj in objs)
                    {
                        _shortCommandRepository.Update(obj);
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

        /// <summary>
        /// Deletes Shorts by the entered IDs
        /// </summary>
        /// <param name="ShortIds"></param>
        public ShortViewModel DeleteShort(DeleteShortsCommand command)
        {

            ShortViewModel uvm = new ShortViewModel();

            try
            {
                lock (thisLock)
                {
                    // we are using unitOfWork but the combination of  
                    // UserRepository.Add(user) and SaveChanges() is not atomic.
                    // multiple threads calling to the following methods might cause us to some insert operations

                    foreach (Int64 item in command.Ids)
                    {
                        _shortCommandRepository.Delete(x => x.RecordKey == item);
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

        public void UpdateJsonDataAllShorts()
        {
            IList<Models.dbo.Short> svm = _shortQueryBL.GetShorts(new ShortQuery() { RecordKey = 0, LUShortAgeRestrictionKey = 0, EnrichData = true }).Shorts.ToList();
            UserViewModel uvm = _userq.GetUsers();
            IEnumerable<Models.LOOKUP.LookupBase> l = _lookupQueryBL.Get("LUShortCategoryTypes", (int)Models.Enums.SysInterfaceLanguageEnum.English, false).Lookups;

            foreach (Models.dbo.Short item in svm)
            {
                AddShortJsonData(item, uvm, l);
            }
        }

        public void UploadSharePicturesAllShorts()
        {
            IEnumerable<Models.dbo.Short> newsvm = _shortQueryBL.GetShorts(new ShortQuery() { RecordKey = 0, LUShortAgeRestrictionKey = 0, EnrichData = true }).Shorts;
            foreach (Models.dbo.Short item in newsvm)
            {
                AddSharePicture(item);
            }
        }

        private void Transfer(CreateShortCommand cols, ShortViewModel uvm)
        {
            if (!IsValidParams(cols, uvm))
                return;

            if (IsShortExist(cols))
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Short already exist"));
                return;
            }
            if (!IsWriterExist(cols))
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Writer not exist"));
                return;
            }

            AddShort(cols, uvm);

            long shortKey = 0;
            IEnumerable<Models.dbo.Short> svm = _shortQueryBL.GetShorts(new ShortQuery() { RecordKey = 0, LUShortAgeRestrictionKey = 0, EnrichData = true }).Shorts;
            if (svm != null && svm.Count() > 0)
            {
                shortKey = svm.Max(x => x.RecordKey);

                AddShrtTags(cols, shortKey);
                AddShortJsonData(svm.FirstOrDefault(x => x.RecordKey == shortKey));
            }
            else
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Short not correctly saved"));
                return;
            }
        }

        private void AddShortJsonData(Models.dbo.Short shorty)
        {
            UserViewModel uvm = _userq.GetUsers();
            IEnumerable<Models.LOOKUP.LookupBase> l = _lookupQueryBL.Get("LUShortCategoryTypes", (int)Models.Enums.SysInterfaceLanguageEnum.English, false).Lookups;
            AddShortJsonData(shorty, uvm, l);
        }

        private void AddShortJsonData(Models.dbo.Short shorty, UserViewModel uvm, IEnumerable<Models.LOOKUP.LookupBase> l)
        {
            Models.dbo.UserAccount writer = uvm.Users.FirstOrDefault(x => x.RecordKey == shorty.WriterUserKey);

            string temp = string.Format("shorts_quote_{0}.jpg", shorty.UniqueGuid);
            shorty.SharePicturePath = @"http://readshortsstoragedev.blob.core.windows.net/sharepicturepath/" + temp;

            Models.dbo.MessageMatchShortUserAccount msg = new Models.dbo.MessageMatchShortUserAccount()
            {
                IsAd = false,
                IsShort = true,
                ShortKey = shorty.RecordKey,
                Title = shorty.Title,
                Quote = shorty.Quote,
                Text = shorty.Text,
                ERTInMiliSeconds = shorty.ERTInMiliSeconds,
                IsPublic = shorty.IsPublic,
                CategoryPicturePath = shorty.CategoryPicturePath,
                SharePicturePath = shorty.SharePicturePath,
                BackgroundPicturePath = shorty.BackgroundPicturePath,
                VideoPath = shorty.VideoPath,
                Embed = shorty.Embed,
                LUShortAgeRestrictionKey = shorty.LUShortAgeRestrictionKey,
                LUShortAgeRestrictionName = shorty.LUShortAgeRestrictionName,
                LUSysInterfacelanguageKey = shorty.LUSysInterfacelanguageKey,
                LUSysInterfacelanguageName = shorty.LUSysInterfacelanguageName,
                QuoteTypeKey = shorty.LUQuoteTypeKey,
                QuoteTypeName = shorty.LUQuoteTypeName,
                StoryTypeKey = shorty.LUStoryTypeKey,
                StoryTypeName = shorty.LUStoryTypeName,
                CategoryTypeKey = shorty.LUCategoryTypeKey,
                CategoryTypeName = shorty.LUCategoryTypeName,
                WriterUserKey = shorty.WriterUserKey,
                WriterUserName = shorty.WriterUserName,
                WriterEmailAddress = writer.EmailAddress,
                WriterFirstName = writer.FirstName,
                WriterLastName = writer.LastName,
                WriterShortBio = writer.ShortBio,
                WriterPicturePath = writer.PicturePath,
                WriterExternalLink = writer.ExternalLink,
                WriterExternalLinkText = writer.ExternalLinkText,
                UserAccountKey = 0,
                UserName = "",
                ShortSignAsLike = false,
                ShortSignAsBookmark = false,
                IsUserAccountWriterFollowed = false
            };

            string url = @"http://readshortsstoragedev.blob.core.windows.net/categorypicturepath/{0}";
            if (string.IsNullOrEmpty(msg.CategoryPicturePath))
            {
                var t = l.FirstOrDefault(x => x.RecordKey == msg.CategoryTypeKey);
                if (t != null)
                {
                    msg.CategoryPicturePath = string.Format(url, t.AditionalData);
                }
            }

            string json = JsonConvert.SerializeObject(msg);
            shorty.JsonData = json;

            Entities.dbo.Short e = base.Map<Models.dbo.Short, Entities.dbo.Short>(shorty);
            _shortCommandRepository.Update(e);
            base.UnitOfWork.SaveChanges();
        }

        private void AddShrtTags(CreateShortCommand cols, long shortKey)
        {
            if (cols.Tags != null)
            {
                string[] arr = cols.Tags.Split(',');
                foreach (string item in arr)
                {
                    string manItem = item.ToLower().Trim();
                    AddTagType(cols, manItem);
                    AddShortTagType(cols, shortKey, manItem);
                }
            }
        }

        private bool IsValidParams(CreateShortCommand cols, ShortViewModel uvm)
        {

            if (string.IsNullOrEmpty(cols.WritersEmail) && cols.WriterUserKey == 0)
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Email and Writer Key are empty"));
                return false;
            }

            if (!string.IsNullOrEmpty(cols.WritersEmail) && cols.WriterUserKey > 0)
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Email and Writer Key are with values ,choose one"));
                return false;
            }

            if (string.IsNullOrEmpty(cols.Text))
            {
                ((List<Message>)uvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Text empty"));
                return false;
            }

            return true;
        }

        private bool IsWriterExist(CreateShortCommand cols)
        {
            UserViewModel usersvm = Task.Run(() => _userq.GetUsers()).Result;
            IEnumerable<Models.dbo.UserAccount> curUsers = usersvm.Users;

            if (curUsers != null && curUsers.Count() > 0)
            {
                if (!string.IsNullOrEmpty(cols.WritersEmail))
                {
                    var c = curUsers.FirstOrDefault(x => x.EmailAddress == cols.WritersEmail.ToLower() && x.LUSubscriptionTypeKey == (int)Enums.LUSubscriptionTypeEnum.WriterPremium);
                    if (c != null)
                    {
                        cols.WriterUserKey = c.RecordKey;
                        return true;
                    }
                }
                else if (cols.WriterUserKey > 0)
                {
                    var c = curUsers.FirstOrDefault(x => x.RecordKey == cols.WriterUserKey && x.LUSubscriptionTypeKey == (int)Enums.LUSubscriptionTypeEnum.WriterPremium);
                    if (c != null)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool IsShortExist(CreateShortCommand cols)
        {
            IEnumerable<Models.dbo.Short> curShorts = _shortQueryBL.GetShorts(new ShortQuery() { RecordKey = 0, LUShortAgeRestrictionKey = 0, EnrichData = false }).Shorts; //_shortq.GetAll();


            if (curShorts != null && curShorts.Count() > 0)
            {
                var c = curShorts.FirstOrDefault(x => x.Text == cols.Text);
                if (c != null)
                    return true;
            }
            return false;
        }

        private void AddShortCategoryType(CreateShortCommand cols, Models.dbo.Short csc)
        {
            Models.LOOKUP.LookupBase val = _lookupQueryBL.Get("LUShortCategoryTypes", csc.LUSysInterfacelanguageKey, false).Lookups.Where(x => x.Description.ToLower() == cols.CategoryType.ToLower()).FirstOrDefault();
            csc.LUCategoryTypeKey = (val != null) ? val.RecordKey : 1;

        }

        private void AddShortTagType(CreateShortCommand cols, long shortKey, string item)
        {
            Models.LOOKUP.LookupBase val = _lookupQueryBL.Get("LUShortTagTypes", cols.LUSysInterfacelanguageKey, false).Lookups.Where(x => x.Description.ToLower() == item.ToLower()).FirstOrDefault();
            if (val != null)
            {
                CreateShortTagCommand cscc = new CreateShortTagCommand();
                cscc.LUShortTagTypeKey = val.RecordKey;
                cscc.ShortKey = shortKey;
                _shortTagCommand.Create(cscc);
            }
        }

        private Models.dbo.Short AddShort(CreateShortCommand cols, ShortViewModel uvm)
        {
            Models.dbo.Short csc = new Models.dbo.Short();

            AddShort(cols, csc);
            AddCategoryType(cols, csc);
            AddShortCategoryType(cols, csc);

            _shortCommandRepository.Add(base.Map<Models.dbo.Short, Entities.dbo.Short>(csc));
            base.UnitOfWork.SaveChanges();


            return csc;
        }

        private static void AddSharePicture(Models.dbo.Short csc)
        {
            string[] myPicTemplates = "red|purple|navy|green|black".Split('|');
            Random rnd = new Random();
            string color = myPicTemplates[rnd.Next(myPicTemplates.Length)];
            string backgroundPhotoTemplateFullPath = string.Format("share_background_template_{0}.png", color);
            string temp = string.Format("shorts_quote_{0}.jpg", csc.UniqueGuid);
            //csc.SharePicturePath = @"http://readshortsstoragedev.blob.core.windows.net/sharepicturepath/" + tempDestinationPhotofullPath;
            string writer = string.Format("{0} {1}", StringExtender.UppercaseFirst(csc.WriterFirstName), StringExtender.UppercaseFirst(csc.WriterLastName));
            WatermarkBL.Execute(backgroundPhotoTemplateFullPath, csc.Quote, writer, temp);
        }

        private static void AddShort(CreateShortCommand cols, Models.dbo.Short csc)
        {
            csc.WriterUserKey = cols.WriterUserKey;
            csc.UniqueGuid = Guid.NewGuid().ToString();
            csc.CategoryPicturePath = cols.CategoryPicturePath;
            csc.Quote = cols.Quote;
            csc.Text = cols.Text;
            csc.LUShortAgeRestrictionKey = cols.LUShortAgeRestrictionKey;
            csc.Title = cols.Title;
            csc.IsPublic = true;
            csc.LUSysInterfacelanguageKey = cols.LUSysInterfacelanguageKey;
            csc.LUQuoteTypeKey = cols.LUQuoteTypeKey;
            csc.LUStoryTypeKey = cols.LUStoryTypeKey;
            csc.ERTInMiliSeconds = csc.Text.Split(' ').Length * 400; // Estimate ERT : 2.5 words per second => 400 ms per word
        }

        private void AddCategoryType(CreateShortCommand cols, Models.dbo.Short csc)
        {
            if (string.IsNullOrEmpty(cols.CategoryType))
            {
                return;
            }

            IEnumerable<Models.LOOKUP.LookupBase> cha = _lookupQueryBL.Get("LUShortCategoryTypes", csc.LUSysInterfacelanguageKey, false).Lookups.Where(x => x.Description.ToLower() == cols.CategoryType.ToLower());
            if (cha == null || (cha != null && cha.Count() == 0))
            {
                CreateLookupCommand clc = new CreateLookupCommand();
                clc.TableName = "LUShortCategoryType";
                clc.Obj = new Models.LOOKUP.LookupBase() { Description = cols.CategoryType };
                _lookupComBL.Create(clc);
            }
        }

        private void AddTagType(CreateShortCommand cols, string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                return;
            }

            IEnumerable<Models.LOOKUP.LookupBase> cha = _lookupQueryBL.Get("LUShortTagTypes", cols.LUSysInterfacelanguageKey, false).Lookups.Where(x => x.Description.ToLower() == item.ToLower());
            if (cha == null || (cha != null && cha.Count() == 0))
            {
                CreateLookupCommand clc = new CreateLookupCommand();
                clc.TableName = "LUShortTagType";
                clc.Obj = new Models.LOOKUP.LUShortTagType() { Description = item };
                _lookupComBL.Create(clc);
            }
        }
    }
}
