using Framework.Core.Interfaces.CQRS;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;

namespace readShorts.Services.QueryHandlers
{
    public sealed class LookupQueryHandler : QueryHandlerBase, IQueryHandler<LookupQuery, LookupViewModel>
    {
        private readonly ILookupQueryBL _lookupQueryBL;

        public LookupQueryHandler(ILookupQueryBL lookupBL)
        {
            _lookupQueryBL = lookupBL;
        }

        public LookupViewModel Retrieve(LookupQuery query)
        {
            if (string.IsNullOrEmpty(query.TableName))
                return null;

            return _lookupQueryBL.Get(query.TableName, query.LUSysInterfaceLanguageKey);
        }
    }
}