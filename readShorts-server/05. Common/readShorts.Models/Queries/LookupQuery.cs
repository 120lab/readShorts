using Framework.Core.Interfaces.CQRS;

namespace readShorts.Models.Queries
{
    public sealed class LookupQuery : IQuery
    {
        public System.Int64 LUSysInterfaceLanguageKey { get; set; }
        public string TableName { get; set; }        
    }
}
