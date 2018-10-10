using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using readShorts.BusinessLogic;
using readShorts.BusinessLogic.Interfaces;
using readShorts.BusinessLogic.ServiceBL;
using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories;
using readShorts.DataAccess.Repositories.commands;
using readShorts.DataAccess.Repositories.dbo;
using readShorts.DataAccess.Repositories.Interfaces.Commands;
using readShorts.DataAccess.Repositories.Interfaces.Queries;
using readShorts.DataAccess.Repositories.Queries;
using System.Collections.Generic;
using System.IO;

namespace UnitTestServices
{
    [TestClass]
    public class ImportShortsUT
    {
        private ImportShortsBL _bl;

        public ImportShortsUT()
        {
            IDatabaseFactory db = new DatabaseFactory();
            IUnitOfWork unitOfWork = new UnitOfWork(db);
            IShortCommandRepository shortCommandRepository = new ShortCommandRepository(db);
            IShortTagCommandRepository shortTagRep = new ShortTagCommandRepository(db);
            //IShortChannelCommandRepository shortChannelRep = new ShortChannelCommandRepository(db);
            ILookupQueryRepository lookupqr = new LookupQueryRepository(db);
            ILookupCommandRepository lookupcm = new LookupCommandRepository(db);
            ILookupQueryBL lookupQ = new LookupQueryBL(lookupqr, unitOfWork, null);
            ILookupCommandBL lookupC = new LookupCommandBL(lookupcm, unitOfWork, null);
            IShortQueryRepository shortQRep = new ShortQueryRepository(db);
            IUserAccountQueryRepository userQueryRep = new UserAccountRepository(db);
            IUserQueryBL userQueryBL = new UserQueryBL(userQueryRep, unitOfWork, null, lookupQ, null);
            IShortQueryBL shortQBL = new ShortQueryBL(shortQRep, unitOfWork, null, lookupqr, userQueryRep );
            IShortCommandBL shortCBL = new ShortCommandBL(shortCommandRepository, lookupQ, shortTagRep, unitOfWork, null, lookupC, userQueryBL, shortQBL);
            _bl = new ImportShortsBL(shortCommandRepository, lookupQ,  shortTagRep, unitOfWork, null, lookupC,shortCBL);
        }

        [TestMethod]
        public void TestImportShorts_ValidFile()
        {
            string path = Directory.GetCurrentDirectory() + @"\..\..\import\";
            string filePath = path + "test_scenario_shorts.csv";
            string resultPath = path + "result_scenario_shorts.csv";
            List<string> errReport = _bl.Execute(filePath);
            File.AppendAllLines(resultPath, errReport);
        }

        [TestMethod]
        public void TestImportShorts_UpdateViewCangesAllShorts()
        {
            _bl.Execute();
        }
    }
}