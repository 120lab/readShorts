using System.Collections.Generic;

namespace readShorts.Models
{
    public class PublicChart
    {
        public IDictionary<long,ChartData> ShortScore { get; set; }
        public IDictionary<long, ChartData> ShortTypeScore { get; set; }
        public IDictionary<long, ChartData> WriterScore { get; set; }
        public IDictionary<long, ChartData> WriterTypeScore { get; set; }
        public IDictionary<long, ChartData> ShortTimeScore { get; set; }
        public IDictionary<long, ChartData> ShortTagScore { get; set; }

    }
}