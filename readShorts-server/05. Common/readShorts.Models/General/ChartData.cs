using System.Collections.Generic;

namespace readShorts.Models
{
    public class ChartData
    {
        public long ShortKey { get; set; }
        public long UserAccountKey { get; set; }
        public long ScoreRead { get; set; }
        public long ScoreLike { get; set; }
        public long ScoreShare { get; set; }
        public long ScoreBookmark { get; set; }
        public long ScoreFollowed { get; set; }

    }
}