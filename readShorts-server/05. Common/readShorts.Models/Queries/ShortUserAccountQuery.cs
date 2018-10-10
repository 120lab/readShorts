using Framework.Core.Interfaces.CQRS;

namespace readShorts.Models.Queries
{
    public sealed class ShortUserAccountQuery : IQuery
    {
        public string UserName { get; set; }
        public readShorts.Models.Enums.ShortsFeedType ShortsFeedTypeItem { get; set; }
    }
}