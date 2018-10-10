using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using readShorts.BusinessLogic.Interfaces;
using readShorts.BusinessLogic.ServiceBL;
using readShorts.Models;
using readShorts.Models.Commands;
using readShorts.Models.dbo;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace readShorts.BusinessLogic
{
    public class MatchAlgoBL : BaseBL, IMatchAlgoBL
    {
        private readonly PublicChart _publicChart;
        private readonly IShortUserAccountQueryBL _shortUserAccountQueryBL;
        private readonly IShortQueryBL _shortQueryBL;
        private readonly IShortUserAccountCommandBL _shortUserAccountCommandBL;
        public MatchAlgoBL(IUnitOfWork unitOfWork, ILoggerService loggerService,
            IShortUserAccountQueryBL shortUserAccountQueryBL, IShortUserAccountCommandBL shortUserAccountCommandBL,
            IShortQueryBL shortQueryBL) : base(unitOfWork, loggerService)
        {
            _shortUserAccountQueryBL = shortUserAccountQueryBL;
            _shortQueryBL = shortQueryBL;
            _shortUserAccountCommandBL = shortUserAccountCommandBL;
            _publicChart = new PublicChart();

            FillData();
        }

        private void FillData()
        {
        }

        public IEnumerable<ShortUserAccount> GetMatch(IDictionary<long, string> shorts, MatchShortUserAccountCommand comm, Int64 user)
        {
            IEnumerable<ShortUserAccount> result = null;
            IEnumerable<long> shortList = null;
            ShortUserAccountViewModel suavm = null;

            if (comm.UserName == "@anonymous")
            {
                shortList = GetAnonymousAlgo(comm, shorts);
                result =    (from long e in shortList
                            select new ShortUserAccount() { ShortKey = e, UserAccountKey = user });
            }

            if (comm.UserName != "@anonymous")
            {
                suavm = _shortUserAccountQueryBL.GetShortsUserAccount(new ShortUserAccountQuery() { UserName = comm.UserName, ShortsFeedTypeItem = comm.ShortsFeedTypeItem });

                if (comm.ShortsFeedTypeItem == readShorts.Models.Enums.ShortsFeedType.Home)
                {
                    if (suavm.ShortUserAccounts.Count() < comm.ShortItemsAmount)
                    {
                        shortList = GetShortsAlgo(shorts, user, comm);
                        _shortUserAccountCommandBL.CreateShortUserAccount(user, shortList.ToList<long>());
                        result =    (from long e in shortList
                                     select new ShortUserAccount() { ShortKey = e, UserAccountKey = user });

                    }
                    else
                    {
                        result = suavm.ShortUserAccounts.Take(comm.ShortItemsAmount);
                    }
                }
                else
                {
                    result = suavm.ShortUserAccounts;
                }
            }

            return result;
        }

        private IEnumerable<long> GetShortsAlgo(IDictionary<long, string> shorts, Int64 user, MatchShortUserAccountCommand comm)
        {
            ShortUserAccountViewModel suavm = _shortUserAccountQueryBL.GetShortsUserAccount(new ShortUserAccountQuery() { UserName = comm.UserName, ShortsFeedTypeItem = Enums.ShortsFeedType.ViewedOnly });

            if (suavm != null && suavm.ShortUserAccounts != null)
            {
                foreach (var item in suavm.ShortUserAccounts)
                {
                    shorts.Remove(item.ShortKey);
                }
            }

            var scoreup = GetRandomalMatch(shorts, comm.ShortItemsAmount);
            var combined = scoreup.Select(i => i.Key);

            //var scoreFollowed = shorts.OrderByDescending(x => x.Value.ScoreFollowed).Take(totalRecordsToMatch * 20 / 100);
            //var scoreLike = shorts.OrderByDescending(x => x.Value.ScoreLike).Take(totalRecordsToMatch * 20 / 100);
            //var scoreRead = shorts.OrderByDescending(x => x.Value.ScoreRead).Take(totalRecordsToMatch * 20 / 100);
            //var scoreShare = shorts.OrderByDescending(x => x.Value.ScoreShare).Take(totalRecordsToMatch * 40 / 100);
            //var combined = shorts.Concat(scoreLike).Concat(scoreRead).Concat(scoreShare).Select(i => i.Key);

            /// TBD
            /// Match A values
            //GetMatchA(shortsvm.shorts);
            /// Match B values
            //GetMatchB(shortsvm.shorts);
            /// PublicChart values
            //GetPublicChartMatch(shortsvm.shorts);
            /// Randomal values

            /// Fill match to ShortUserAccount table
            return combined;
        }

        private IEnumerable<long> GetAnonymousAlgo(MatchShortUserAccountCommand comm, IDictionary<long, string> shorts)
        {
            IEnumerable<KeyValuePair<long, string>> v = GetRandomalMatch(shorts, comm.ShortItemsAmount);

            return Enumerable.ToList(v.Select(x => x.Key));
        }

        private IEnumerable<KeyValuePair<long, string>> GetRandomalMatch(IDictionary<long, string> shorts, int randNumberOfElements)
        {
            Random rand = new Random();
            var v = shorts.Select(y => y.Key);
            //Dictionary<long, long> values = v.ToDictionary(i => i + 1, x => x);

            var r = shorts.OrderBy(x => rand.Next(shorts.Count)).Take(randNumberOfElements);

            return r;
        }
    }
}