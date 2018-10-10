using System.Linq;

namespace readShorts.DataAccess.Interfaces
{
    using Entities.dbo
        ;
    using Framework.DataAccess.Interfaces;
    using System;
    using System.Collections.Generic;

    public interface IShortUserAccountQueryRepository : IReadOnlyRepository<ShortUserAccount>
    {
        ShortUserAccount GetShortUserAccount(Int64 recordKey);

        ShortUserAccount GetShortUserAccount(Int64 userAccountKey, Int64 ShortKey);

        //IEnumerable<ShortUserAccount> GetShortUserAccounts(Int64 WriterKey);

        IEnumerable<ShortUserAccount> GetShortUserAccountsSimilarWriter(Int64 userAccountKey, Int64 shortKey);

        IEnumerable<ShortUserAccount> GetShortUserAccounts(DateTime interval);

        IEnumerable<ShortUserAccount> GetShortUserAccountsViewed(Int64 userAccountKey);

        IEnumerable<ShortUserAccount> GetShortUserAccountsNotViewed(Int64 userAccountKey);

        IEnumerable<ShortUserAccount> GetShortUserAccountsBookmarked(Int64 userAccountKey);

        IEnumerable<ShortUserAccount> GetShortUserAccountsFollowedWriters(Int64 userAccountKey);

        IEnumerable<ShortUserAccount> GetShortUserAccountsTopLiked(Int64 userAccountKey);

        IEnumerable<ShortUserAccount> GetShortUserAccountsLiked(Int64 userAccountKey);

        IEnumerable<ShortUserAccount> GetShortUserAccountsFriendsShares(Int64 userAccountKey);

        IEnumerable<ShortUserAccount> GetShortUserAccounts(Int64 userAccountKey);

    }
}