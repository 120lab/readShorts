using System;
using System.Linq;
using System.Collections.Generic;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using readShorts.Models.dbo;
using readShorts.Models.Commands;
using readShorts.Models;
using readShorts.BusinessLogic.ServiceBL;
using System.Net.Mail;

namespace readShorts.BusinessLogic
{
    public class GeneralTasksBL : BaseBL, IGeneralTasksBL
    {
        private const int CONST_CALM_DOWN_SHORTS_VIEW_UBOUND = 6;
        private const string CONST_CALM_DOWN_MESSAGE = "Calm down, you viewed {0} shorts in one minute";
        private const int CONST_MINUTES_TO_TEST_CALM = -3;
        private readonly IShortUserAccountQueryBL _shortUserAccountQueryBL;

        public GeneralTasksBL(IShortUserAccountQueryBL shortUserAccountQueryBL,
            IUnitOfWork unitOfWork, ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _shortUserAccountQueryBL = shortUserAccountQueryBL;
        }

        public GeneralTasksViewModel Execute(GeneralTasksCommand query)
        {
            GeneralTasksViewModel uabvm = new GeneralTasksViewModel();
            bool result = false;

            switch (query.CurrentTask)
            {
                case GeneralTasksCommand.GeneralTask.SendContactMail:
                    MailMessage contactMsg = SendMailBL.CreateContactMail(query.Message);
                    result = SendMailBL.Execute(contactMsg);
                    break;
                case GeneralTasksCommand.GeneralTask.SendImageForText:

                    string tempDestinationPhotofullPath = string.Format("social_network_quotes_{0}.jpg", Guid.NewGuid());
                    WatermarkBL.Execute(query.ImageFullPath, query.Message, query.Writer, tempDestinationPhotofullPath);
                    tempDestinationPhotofullPath = @"http://readshortsstoragedev.blob.core.windows.net/sharepicturepath/" + tempDestinationPhotofullPath;
                    MailMessage imageMsg = SendMailBL.CreateImageForTextMail(query.Subject, tempDestinationPhotofullPath);
                    result = SendMailBL.Execute(imageMsg);
                    break;
                default:
                    break;
            }

            if (result)
            {
                ((List<Message>)uabvm.Messages).Add(new Models.Message(Models.LogLevel.Info, "Contact Mail sent"));
            }
            else
            {
                ((List<Message>)uabvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Contact Mail fail"));
            }

            return uabvm;
        }

        public GeneralTasksViewModel Execute(GeneralTasksQuery query)
        {
            GeneralTasksViewModel uabvm = new GeneralTasksViewModel();
            bool result = false;

            switch (query.CurrentTask)
            {
                case GeneralTasksQuery.GeneralTask.SendContactMail:
                    MailMessage contactMsg = SendMailBL.CreateContactMail(query.Message);
                    result = SendMailBL.Execute(contactMsg);
                    break;
                case GeneralTasksQuery.GeneralTask.SendImageForText:

                    string tempDestinationPhotofullPath = string.Format("social_network_quotes_{0}.jpg", Guid.NewGuid());
                    WatermarkBL.Execute(query.ImageFullPath, query.Message, query.Writer, tempDestinationPhotofullPath);
                    tempDestinationPhotofullPath = @"http://readshortsstoragedev.blob.core.windows.net/sharepicturepath/" + tempDestinationPhotofullPath;
                    MailMessage imageMsg = SendMailBL.CreateImageForTextMail(query.Subject, tempDestinationPhotofullPath);
                    result = SendMailBL.Execute(imageMsg);
                    break;
                default:
                    break;
            }

            if (result)
            {
                ((List<Message>)uabvm.Messages).Add(new Models.Message(Models.LogLevel.Info, "Contact Mail sent"));
            }
            else
            {
                ((List<Message>)uabvm.Messages).Add(new Models.Message(Models.LogLevel.Error, "Contact Mail fail"));
            }

            return uabvm;
        }


    }
}
