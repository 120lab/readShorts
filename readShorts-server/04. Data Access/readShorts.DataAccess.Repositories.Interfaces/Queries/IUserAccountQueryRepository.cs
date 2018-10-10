using System.Linq;

namespace readShorts.DataAccess.Interfaces
{
    using Entities.dbo
        ;
    using Framework.DataAccess.Interfaces;
    using System.Threading.Tasks;

    public interface IUserAccountQueryRepository : IRepository<UserAccount>
    {
        IQueryable<UserAccount> GetAllUserAccountsAsync();
        IQueryable<UserAccount> GetAllUserAccounts();

        UserAccount GetEveryUserByUserNameAsync(string userName);
        UserAccount GetEveryUserByUserName(string userName);

        UserAccount GetLoginVerifiedUserAsync(string identity, string password);
        UserAccount GetLoginVerifiedUser(string identity, string password);

        UserAccount GetLoginVerifiedUserIdentityOnlyAsync(string identity);
        UserAccount GetLoginVerifiedUserIdentityOnly(string identity);

        UserAccount GetEveryUserByGuidAsync(string guid);
        UserAccount GetEveryUserByGuid(string guid);
    }
}