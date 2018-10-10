using System;
using System.Linq;
using System.Collections.Generic;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using readShorts.DataAccess.Repositories.Interfaces.Queries;
using readShorts.DataAccess.Interfaces;

namespace readShorts.BusinessLogic
{
    public class AdQueryBL : BaseBL, IAdQueryBL
    {
        private readonly IAdQueryRepository _adRepository;
        private readonly ILookupQueryRepository _lookupRepository;

        public AdQueryBL(IAdQueryRepository adQueryRepository, ILookupQueryRepository lookupRepository,
            IUnitOfWork unitOfWork, ILoggerService loggerService)
            : base(unitOfWork, loggerService)
        {
            _adRepository = adQueryRepository;
            _lookupRepository = lookupRepository;
        }

        public AdViewModel GetAdsFullData(Int64 lUSysInterfaceLanguageKey, int adsCount)
        {
            IEnumerable<Entities.dbo.Ad> entities = _adRepository.GetMany(x => x.IsRowDeleted == false).AsEnumerable();
            AdViewModel adVM = new AdViewModel();
            adVM.Ads = base.Map<IEnumerable<readShorts.Entities.dbo.Ad>, IEnumerable<readShorts.Models.dbo.Ad>>(entities);

            IQueryable<Entities.LOOKUP.LUCountry> countries = _lookupRepository.GetLUCountries(lUSysInterfaceLanguageKey);
            IQueryable<Entities.LOOKUP.LUSysInterfaceLanguage> languages = _lookupRepository.GetLUSysInterfaceLanguages(lUSysInterfaceLanguageKey);
            IQueryable<Entities.LOOKUP.LUShortAgeRestriction> ages = _lookupRepository.GetLUShortAgeRestrictions(lUSysInterfaceLanguageKey);
            adVM.Ads = (from ad in adVM.Ads
                        join c in countries on ad.LUCountryKey equals c.RecordKey into leftjoinCountries
                        from ljc in leftjoinCountries.DefaultIfEmpty()
                        join l in languages on ad.LUSysInterfacelanguageKey equals l.RecordKey
                        //join a in ages on ad.LUShortAgeRestrictionKey equals a.RecordKey
                        select new Models.dbo.Ad
                        {
                            CompanyName = ad.CompanyName,
                            Interval = ad.Interval,
                            AdPath = ad.AdPath,
                            AdBody = ad.AdBody,
                            LUShortAgeRestrictionKey = ad.LUShortAgeRestrictionKey,                            
                            LUSysInterfacelanguageKey = ad.LUSysInterfacelanguageKey,
                            LUSysInterfacelanguageName = l.Description,
                            LUCountryKey = ad.LUCountryKey,
                            LUCountryName = (ljc == null ?string.Empty : ljc.Description)
                        }).Take(adsCount);
            return adVM;
        }
    }
}
