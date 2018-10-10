using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;
using System.Collections.Generic;

namespace readShorts.Models.ViewModels
{
    public sealed class MessageMatchShortUserAccountViewModel : BaseViewModel, IQueryResult
    {
        public MessageMatchShortUserAccountViewModel()
        {
            Matches = new List<MessageMatchShortUserAccount>();
            NetworkData = new List<UserMatchNetworkData>();
        }

        public int PLMData { get; set; }
        public IEnumerable<MessageMatchShortUserAccount> Matches { get; set; }
        public IEnumerable<UserMatchNetworkData> NetworkData { get; set; }

        public class UserMatchNetworkData
        {
            public string PicturePath { get; set; }
            public string MatchPercentage { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }
    }
}