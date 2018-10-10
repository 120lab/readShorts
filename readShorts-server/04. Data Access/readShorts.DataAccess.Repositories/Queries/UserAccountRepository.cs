using readShorts.DataAccess.Interfaces;
using readShorts.Entities.dbo;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace readShorts.DataAccess.Repositories.dbo
{
    public class UserAccountRepository : RepositoryBase<UserAccount>, IUserAccountQueryRepository
    {
        public UserAccountRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IQueryable<UserAccount> GetAllUserAccountsAsync()
        {
            var res = DataContext.UserAccounts.ToListAsync().Result;
            return res.AsQueryable();
        }

        public IQueryable<UserAccount> GetAllUserAccounts()
        {
            var res = dbset.Where(x => x.IsRowDeleted == false);

            return res;
        }


        public UserAccount GetEveryUserByUserNameAsync(string userName)
        {
            var res = DataContext.UserAccounts.FirstOrDefaultAsync(x => x.UserName == userName && x.IsRowDeleted == false).Result;
            return res;
        }

        public UserAccount GetEveryUserByUserName(string userName)
        {
            var result = dbset.FirstOrDefault(
                x => x.UserName == userName &&
                x.IsRowDeleted == false);

            return result;
        }

        public UserAccount GetLoginVerifiedUserAsync(string identity, string password)
        {
            var res = DataContext.UserAccounts.FirstOrDefaultAsync(x => x.EmailAddress == identity &&
               x.HashedPassword == password &&
               x.LUUserVerificationTypeKey == 4 /*Verified*/ &&
               x.IsRowDeleted == false).Result;
            return res;
        }

        public UserAccount GetLoginVerifiedUser(string identity, string password)
        {
            return dbset.FirstOrDefault(
                x => x.EmailAddress == identity &&
                x.HashedPassword == password &&
                x.LUUserVerificationTypeKey == 4 /*Verified*/ &&
                x.IsRowDeleted == false);
        }

        public UserAccount GetLoginVerifiedUserIdentityOnlyAsync(string identity)
        {
            var res = DataContext.UserAccounts.FirstOrDefaultAsync(
               x => x.EmailAddress == identity &&
                x.LUUserVerificationTypeKey == 4 /*Verified*/ &&
                x.IsRowDeleted == false).Result;
            return res;
        }

        public UserAccount GetLoginVerifiedUserIdentityOnly(string identity)
        {
            return dbset.FirstOrDefault(
                x => x.EmailAddress == identity &&
                x.LUUserVerificationTypeKey == 4 /*Verified*/ &&
                x.IsRowDeleted == false);
        }

        public UserAccount GetEveryUserByGuidAsync(string guid)
        {
            var res = DataContext.UserAccounts.FirstOrDefaultAsync(
               x => x.UniqueGuid == guid &&
                x.IsRowDeleted == false).Result;
            return res;
        }

        public UserAccount GetEveryUserByGuid(string guid)
        {
            var result = dbset.FirstOrDefault(
                x => x.UniqueGuid == guid &&
                x.IsRowDeleted == false);

            return result;
        }
    }
}