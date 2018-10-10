using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.Commands
{
    public sealed class MatchShortUserAccountCommand : ICommand
    {
        public const string CONST_USER_NAME_FIELDNAME = "User Name";
        public const string CONST_SHORT_ITEMS_AMOUNT_FIELDNAME = "Short Items Amount";
        public const string CONST_RETURN_JUST_BOOKMARKED_ITEMS = "Return Just Bookmarked Items";

        public string UserName { get; set; }
        public int ShortItemsAmount { get; set; }
        public readShorts.Models.Enums.ShortsFeedType ShortsFeedTypeItem { get; set; }
        public long? FirstShortKey { get; set; }
    }
}