namespace readShorts.DataAccess.Repositories.Interfaces.Queries
{
    using Entities.LOOKUP;
    using Framework.DataAccess.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface ILookupQueryRepository : IReadOnlyRepository<LookupBase>
    {
        IQueryable<LUActivity> GetLUActivities(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUCountry> GetLUCountries(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUEventType> GetLUEventTypes(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUGender> GetLUGenders(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUGroup> GetLUGroups(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUPointType> GetLUPointTypes(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUShortAgeRestriction> GetLUShortAgeRestrictions(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUShortCategoryType> GetLUShortCategoryTypes(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUShortReportType> GetLUShortReportTypes(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUShortShareType> GetLUShortShareTypes(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUShortTagType> GetLUShortTagTypes(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUSubscriptionType> GetLUSubscriptionTypes(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUSysInterfaceLanguage> GetLUSysInterfaceLanguages(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUQuoteType> GetLUQuoteType(Int64 lUSysInterfaceLanguageKey);

        IQueryable<LUStoryType> GetLUStoryType(Int64 lUSysInterfaceLanguageKey);
    }
}