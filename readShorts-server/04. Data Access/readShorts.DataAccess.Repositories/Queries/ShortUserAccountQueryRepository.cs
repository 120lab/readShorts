using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Queries;
using readShorts.Entities.dbo;
using readShorts.Entities.LOOKUP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using Framework.Core.Utils;

namespace readShorts.DataAccess.Repositories.Queries
{
    public class ShortUserAccountQueryRepository : ReadOnlyRepositoryBase<ShortUserAccount>, IShortUserAccountQueryRepository
    {
        private const int CONST_COUNT_TOP_LIKED = 25;

        public ShortUserAccountQueryRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

        //  public IEnumerable<ShortUserAccount> GetShortUserAccounts(Int64 WriterKey)
        //  {
        //      ShortUserAccount result = dataContext.ShortUserAccounts.FirstOrDefault(x => x.wr == WriterKey &&
        //x.IsRowDeleted == false);

        //      return result;
        //  }

        public ShortUserAccount GetShortUserAccount(Int64 userAccountKey, Int64 shortKey)
        {
            ShortUserAccount result = dataContext.ShortUserAccounts.FirstOrDefault(x => x.UserAccountKey == userAccountKey &&
                x.ShortKey == shortKey &&
                x.IsRowDeleted == false);

            return result;
        }

        public ShortUserAccount GetShortUserAccount(Int64 recordKey)
        {
            ShortUserAccount result = dataContext.ShortUserAccounts.FirstOrDefault(x => x.RecordKey == recordKey && x.IsRowDeleted == false);

            return result;
        }

        public IEnumerable<ShortUserAccount> GetShortUserAccounts(DateTime interval)
        {
            IEnumerable<ShortUserAccount> result = from sua in dataContext.ShortUserAccounts
                                                   where sua.IsRowDeleted == false &&
                                                   sua.CreatedTimeStamp >= interval &&
                                                   sua.CreatedTimeStamp <= DateTime.UtcNow
                                                   select sua;

            return result;
        }

        public IEnumerable<ShortUserAccount> GetShortUserAccounts(Int64 userAccountKey)
        {
            IEnumerable<ShortUserAccount> result = from sua in dataContext.ShortUserAccounts
                                                   where sua.IsRowDeleted == false &&
                                                   sua.UserAccountKey == userAccountKey
                                                   select sua;
            return result;
        }

        public IEnumerable<ShortUserAccount> GetShortUserAccountsSimilarWriter(Int64 userAccountKey, Int64 shortKey)
        {
            IEnumerable<ShortUserAccount> result = null;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "spGetShortUserAccountsSimilarWriter";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@userAccountKey", userAccountKey));
            cmd.Parameters.Add(new SqlParameter("@shortKey", shortKey));
            try
            {
                dataContext.Database.Connection.Open();
                cmd.Connection = (SqlConnection)dataContext.Database.Connection;
                var d = new List<Int64>();
                using (SqlDataReader rslt = cmd.ExecuteReader())
                {
                    while (rslt.Read())
                    {
                        d.Add(rslt.GetInt64(0));
                    }
                }
                result = from sua in dataContext.ShortUserAccounts
                         join u in d
                         on sua.RecordKey equals u
                         select sua;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dataContext.Database.Connection.Close();
            }

            return result;
        }

        public IEnumerable<ShortUserAccount> GetShortUserAccountsViewed(Int64 userAccountKey)
        {
            IEnumerable<ShortUserAccount> result = from sua in dataContext.ShortUserAccounts
                                                   where sua.IsRowDeleted == false &&
                                                   sua.UserAccountKey == userAccountKey &&
                                                   sua.ShortViewByUser == true
                                                   select sua;
            return result;
        }

        public IEnumerable<ShortUserAccount> GetShortUserAccountsNotViewed(Int64 userAccountKey)
        {
            IEnumerable<ShortUserAccount> result = from sua in dataContext.ShortUserAccounts
                                                   where sua.IsRowDeleted == false &&
                                                   sua.UserAccountKey == userAccountKey &&
                                                   sua.ShortSendToUser == true &&
                                                   sua.ShortViewByUser == false
                                                   select sua;
            return result;
        }

        public IEnumerable<ShortUserAccount> GetShortUserAccountsBookmarked(Int64 userAccountKey)
        {
            IEnumerable<ShortUserAccount> result = from sua in dataContext.ShortUserAccounts
                                                   where sua.IsRowDeleted == false &&
                                                   sua.UserAccountKey == userAccountKey &&
                                                   sua.ShortSignAsBookmark == true
                                                   select sua;
            return result;
        }

        public IEnumerable<ShortUserAccount> GetShortUserAccountsTopLiked(Int64 userAccountKey)
        {
            var result = dataContext.ShortUserAccounts.GroupBy(x => x.ShortKey).Select
                        (group => new
                        {
                            ShortKey = group.Key,
                            Count = group.Count()
                        }).OrderByDescending(x => x.Count).Take(CONST_COUNT_TOP_LIKED);

            List<ShortUserAccount> top = new List<ShortUserAccount>();
            foreach (var item in result)
            {
                top.Add(new ShortUserAccount() { ShortKey = item.ShortKey });
            }

            return top;
        }

        public IEnumerable<ShortUserAccount> GetShortUserAccountsLiked(Int64 userAccountKey)
        {
            IEnumerable<ShortUserAccount> result = from sua in dataContext.ShortUserAccounts
                                                   where sua.IsRowDeleted == false &&
                                                   sua.UserAccountKey == userAccountKey &&
                                                   sua.ShortSignAsLike == true
                                                   select sua;
            return result;
        }

        public IEnumerable<ShortUserAccount> GetShortUserAccountsFriendsShares(Int64 userAccountKey)
        {
            IEnumerable<ShortUserAccount> result = null;
            /// TBD
            return result;
        }

        public IEnumerable<ShortUserAccount> GetShortUserAccountsFollowedWriters(Int64 userAccountKey)
        {
            //IEnumerable<ShortUserAccount> result = null;

            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "spGetShortUserAccountsFollowedWriters";
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@userKey", userAccountKey));
            //try
            //{
            //    dataContext.Database.Connection.Open();
            //    cmd.Connection = (SqlConnection)dataContext.Database.Connection;
            //    var d = new Dictionary<Int64,Int64>();
            //    using (SqlDataReader rslt = cmd.ExecuteReader())
            //    {
            //        while (rslt.Read())
            //        {
            //            d.Add(rslt.GetInt64(0), rslt.GetInt64(1));
            //        }
            //    }
            //    result = from sua in dataContext.ShortUserAccounts
            //             join u in d.Keys
            //             on sua.ShortKey equals u
            //             where sua.IsRowDeleted == false
            //             select sua;

            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    dataContext.Database.Connection.Close();
            //}

            IEnumerable<ShortUserAccount> result = from sua in dataContext.ShortUserAccounts
                                                   where sua.IsRowDeleted == false &&
                                                   sua.UserAccountKey == userAccountKey &&
                                                   sua.UserSignWriterAsFollowed == true
                                                   select sua;
            return result;
        }


    }
}