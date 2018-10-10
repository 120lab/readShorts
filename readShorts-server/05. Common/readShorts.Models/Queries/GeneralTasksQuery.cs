using Framework.Core.Interfaces.CQRS;

namespace readShorts.Models.Queries
{
    public sealed class GeneralTasksQuery : IQuery
    {
        public string Message { get; set; }
        public string Subject { get; set; }
        public string ImageFullPath { get; set; }
        public string Writer { get; set; }

        public enum GeneralTask {
            SendContactMail,
            SendImageForText
        }
        public GeneralTask CurrentTask { get; set; }

    }
}
