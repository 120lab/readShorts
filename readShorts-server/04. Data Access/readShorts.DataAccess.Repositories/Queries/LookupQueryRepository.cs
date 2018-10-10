using readShorts.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Queries;
using readShorts.Entities.LOOKUP;
using System;
using System.Collections.Generic;
using System.Linq;

namespace readShorts.DataAccess.Repositories.Queries
{
    public class LookupQueryRepository : ReadOnlyRepositoryBase<LookupBase>, ILookupQueryRepository
    {
        public LookupQueryRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IQueryable<LUActivity> GetLUActivities(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUActivities;
        }

        public IQueryable<LUCountry> GetLUCountries(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUCountries;
        }

        public IQueryable<LUEventType> GetLUEventTypes(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUEventTypes;
        }

        public IQueryable<LUGender> GetLUGenders(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUGenders;
        }

        public IQueryable<LUGroup> GetLUGroups(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUGroups;
        }

        public IQueryable<LUPointType> GetLUPointTypes(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUPointTypes;
        }

        public IQueryable<LUShortAgeRestriction> GetLUShortAgeRestrictions(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUShortAgeRestrictions;
        }

        public IQueryable<LUShortCategoryType> GetLUShortCategoryTypes(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUShortCategoryTypes;
        }

        public IQueryable<LUShortReportType> GetLUShortReportTypes(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUShortReportTypes;
        }

        public IQueryable<LUShortShareType> GetLUShortShareTypes(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUShortShareTypes;
        }

        public IQueryable<LUShortTagType> GetLUShortTagTypes(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUShortTagTypes;
        }

        public IQueryable<LUSubscriptionType> GetLUSubscriptionTypes(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUSubscriptionTypes;
        }

        public IQueryable<LUSysInterfaceLanguage> GetLUSysInterfaceLanguages(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUSysInterfaceLanguages;
        }

        public IQueryable<LUQuoteType> GetLUQuoteType(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUQuoteType;
        }

        public IQueryable<LUStoryType> GetLUStoryType(Int64 lUSysInterfaceLanguageKey)
        {
            return dataContext.LUStoryType;
        }
    }
}