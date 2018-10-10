using Microsoft.VisualStudio.TestTools.UnitTesting;
using readShorts.DataAccess;
using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories;
using readShorts.DataAccess.Repositories.dbo;
using readShorts.Entities.dbo;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace readShorts.Server.DataAccess.Testing
{
    [TestClass]
    public class UnitTestDA
    {
        [NonSerialized]
        private readonly IUserAccountQueryRepository _UserRep;

        private readonly DataContext _DataContext;
        private readonly IDatabaseFactory _dbFactory;

        public UnitTestDA()
        {
            _dbFactory = new DatabaseFactory();
            _UserRep = new UserAccountRepository(_dbFactory);
            _DataContext = new DataContext();
        }

        [TestMethod]
        public void TestMethod_AddFirstDemoUser()
        {
            var users = new[] {
                new UserAccount {
                    HashedPassword ="password", IsAnonimousConnect = false,
                    IsEmailConnect =true, IsFBConnect = false, IsGGLConnect=false,
                    IsTWConnect=false, LastActitiyDate = DateTime.UtcNow,

                    MobileSerialNumber ="", UserSecurityNumber = "",
                    EmailAddress = "idan.gvili@gmail.com", ClientIP="127.0.0.0",
                    UserName = "@User",FirstName = "Test", LastName = "Tester",
                    IsRowDeleted = false}
            };

            _DataContext.UserAccounts.AddOrUpdate(
               u => u.UserName,
               users[0]);

            _DataContext.SaveChanges();
        }

        [TestMethod]
        public void TestMethod_UpdateDemoUser()
        {
            UserAccount user = _UserRep.GetEveryUserByUserName("@User");

            user.ClientIP = "127.0.0.1";
            _DataContext.UserAccounts.AddOrUpdate(
               u => u.RecordKey,
               user);

            _DataContext.SaveChanges();
        }

        [TestMethod]
        public void TestMethod_GetUsers()
        {
            IQueryable<UserAccount> users = _UserRep.GetAll();

            Assert.AreNotEqual(users.Count(), 0);
        }

        [TestMethod]
        public void TestMethod_GetUserByUserName()
        {
            UserAccount user = _UserRep.GetEveryUserByUserName("@User");

            Assert.AreNotEqual(user, null);
        }
    }
}