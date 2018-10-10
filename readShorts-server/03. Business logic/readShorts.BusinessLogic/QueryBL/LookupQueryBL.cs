using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using readShorts.BusinessLogic.Interfaces;
using readShorts.BusinessLogic.ServiceBL;
using readShorts.DataAccess.Repositories.Interfaces.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace readShorts.BusinessLogic
{
    public class LookupQueryBL : BaseBL, ILookupQueryBL
    {
        [NonSerialized]
        private readonly ILookupQueryRepository _lookupQueryRepository;

        public LookupQueryBL(ILookupQueryRepository lookupRepository, IUnitOfWork unitOfWork, ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _lookupQueryRepository = lookupRepository;
        }

        public LookupViewModel Get(string tableName, Int64 lUSysInterfaceLanguageKey)
        {
            return Get(tableName, lUSysInterfaceLanguageKey, true);
        }

        public LookupViewModel Get(string tableName, Int64 lUSysInterfaceLanguageKey, bool useCache)
        {
            string cacheKey = string.Format("Class_{0}_Method_{1}_Param_{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, tableName + "_" + lUSysInterfaceLanguageKey);

            var cacheData = CacheHandler.Instance.GetFromCache(cacheKey);

            if (cacheData != null && useCache)
            {
                LookupViewModel cached = JsonConvert.DeserializeObject<LookupViewModel>(cacheData.ToString());
                return cached;
            }
            else
            {
                object[] param = new object[1] { lUSysInterfaceLanguageKey };

                IEnumerable<Entities.LOOKUP.LookupBase> entities =
                    (IEnumerable<Entities.LOOKUP.LookupBase>)_lookupQueryRepository.GetType().GetMethod(string.Format("Get{0}", tableName)).Invoke(_lookupQueryRepository, param);

                LookupViewModel lookupVM = new LookupViewModel();
                lookupVM.TableName = tableName;
                lookupVM.Lookups = base.Map<IEnumerable<Entities.LOOKUP.LookupBase>, IEnumerable<readShorts.Models.LOOKUP.LookupBase>>(entities);

                CacheHandler.Instance.AddToCache(cacheKey, JObject.FromObject(lookupVM), CacheHandler.ObjectPolicy.LookupTables);

                return lookupVM;

            }
        }
    }
}