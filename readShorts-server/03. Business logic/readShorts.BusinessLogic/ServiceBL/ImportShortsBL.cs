using Framework.Core.Interfaces.Log;
using Framework.Core.ReadFiles;
using Framework.DataAccess.Interfaces;
using readShorts.BusinessLogic.Interfaces;
using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.Models.Commands;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace readShorts.BusinessLogic.ServiceBL
{
    public class ImportShortsBL
    {
        private enum ColIndexes
        {
            WriterKey,
            WriterEmail,
            Title, Text,
            Quote,
            QuoteType,
            AgeRestriction,
            Language,
            StoryType,
            MultiTags,
            Category,
            OtherCategory,
            PictureURL
        }

        private readonly IShortCommandBL _shortCommand;

        public ImportShortsBL(IShortCommandRepository shortCommandRepository, ILookupQueryBL lookupQueryBL,
            IShortTagCommandRepository shortTagRep,/*IShortChannelCommandRepository shortChannelRep, */
            IUnitOfWork unitOfWork, ILoggerService loggerService, ILookupCommandBL lookupComBL, IShortCommandBL shortCbl)
        {
            _shortCommand = shortCbl;
        }

        public void Execute()
        {
            _shortCommand.UpdateJsonDataAllShorts();
            _shortCommand.UploadSharePicturesAllShorts();
        }

        public List<string> Execute(string filePath)
        {
            CsvFileReader c = new CsvFileReader(filePath, Framework.Core.Interfaces.ReadFiles.EmptyLineBehavior.Ignore);
            List<string> cols = new List<string>();
            List<string> errReport = new List<string>();
            int rows = 0;

            while (c.ReadRow(cols))
            {
                try
                {
                    rows++;
                    if (rows == 1)
                    {
                        /// headers
                        if (!Validate(cols))
                        {
                            throw new Exception("line 0 , File structure invalid");
                        }
                    }
                    else
                    {
                        Transfer(cols, rows);
                        errReport.Add(string.Format("line {0} , Created well", rows));
                    }
                }
                catch (Exception e)
                {
                    errReport.Add(e.Message);
                }
            }

            return errReport;
        }

        private void Transfer(List<string> cols, int rows)
        {
            long quoteType = 1;
            switch (cols[(int)ColIndexes.QuoteType].ToLower())
            {
                case "first paragraph":
                    quoteType = 1;
                    break;
                case "part of the story":
                    quoteType = 2;
                    break;
                case "the entire story":
                    quoteType = 3;
                    break;
                default:
                    throw new InvalidDataException(string.Format("line {0} , QuoteType value {1}", rows, cols[(int)ColIndexes.QuoteType].ToLower()));
            }

            long age = 1;
            switch (cols[(int)ColIndexes.AgeRestriction].ToLower())
            {
                case "all stories (13+)":
                    age = 1;
                    break;
                case "restricted (16+)":
                    age = 2;
                    break;
                case "adult contect (18+)":
                    age = 3;
                    break;
                default:
                    throw new InvalidDataException(string.Format("line {0} , AgeRestriction value {1}", rows, cols[(int)ColIndexes.AgeRestriction].ToLower()));

            }

            long language = 2;

            //switch (cols[(int)ColIndexes.Language].ToLower())
            //{
            //    case "english":
            //        language = 1;
            //        break;
            //    case "עברית":
            //        language = 2;
            //        break;
            //    default:
            //        throw new InvalidDataException(cols[(int)ColIndexes.Language].ToLower());

            //}


            long storyType = 1;
            switch (cols[(int)ColIndexes.StoryType].ToLower())
            {
                case "poems":
                    storyType = 1;
                    break;
                case "shorts":
                    storyType = 2;
                    break;
                case "micro shorts":
                    storyType = 3;
                    break;
                case "stories":
                    storyType = 4;
                    break;
                default:
                    throw new InvalidDataException(string.Format("line {0} , StoryType value {1}", rows, cols[(int)ColIndexes.StoryType].ToLower()));

            }

            CreateShortCommand csc = new CreateShortCommand()
            {
                WriterUserKey = Convert.ToInt64(cols[(int)ColIndexes.WriterKey]),
                //WritersEmail = Convert.ToString(cols[(int)ColIndexes.WriterEmail]),
                Title = Convert.ToString(cols[(int)ColIndexes.Title]),
                Text = Convert.ToString(cols[(int)ColIndexes.Text]),
                Quote = Convert.ToString(cols[(int)ColIndexes.Quote]),
                LUQuoteTypeKey = quoteType,
                LUShortAgeRestrictionKey = age,
                LUStoryTypeKey = storyType,
                LUSysInterfacelanguageKey = language,
                Tags = Convert.ToString(cols[(int)ColIndexes.MultiTags]),
                CategoryType = Convert.ToString(cols[(int)ColIndexes.Category]),
                CategoryPicturePath = Convert.ToString(cols[(int)ColIndexes.PictureURL]),

            };
            //csc.WritersEmail = "";
            ShortViewModel svm = _shortCommand.CreateShort(csc);

            if (svm.Messages.Count() > 0)
            {
                string msg = string.Empty;
                foreach (Models.Message item in svm.Messages)
                {
                    msg += string.Format("line {0} , Creation process : Message level : {1}, messge : {2}\r\n", rows, item.LogLevel.ToString(), item.Text);
                }
                throw new InvalidDataException(msg);
            }
        }


        private bool Validate(List<string> cols)
        {
            return (cols.Count() == 13) ? true : false;
        }
    }
}