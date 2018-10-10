using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;

namespace readShorts.Models.Commands
{
    public sealed class CreateShortCommand : ICommand
    {
        public long WriterUserKey { get; set; }
        public string WritersEmail { get; set; }
        public string Title { get; set; }
        public string Quote { get; set; }
        public string Text { get; set; }
        public long LUShortAgeRestrictionKey { get; set; }
        public long LUSysInterfacelanguageKey { get; set; }
        public long LUQuoteTypeKey { get; set; }
        public long LUStoryTypeKey { get; set; }
        public string Tags { get; set; }
        public string CategoryType { get; set; }
        public string CategoryPicturePath { get; set; }

    }
}
