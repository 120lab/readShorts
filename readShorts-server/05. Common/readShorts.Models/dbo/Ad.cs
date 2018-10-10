using System;

namespace readShorts.Models.dbo
{
    public class Ad : DboBase
    {
        public virtual string CompanyName { get; set; }
        public virtual int Interval { get; set; }
        public virtual string AdPath { get; set; }
        public virtual string AdBody { get; set; }

        /// FK
        public Int64? LUShortAgeRestrictionKey { get; set; }
        public string LUShortAgeRestrictionName { get; set; }
        public Int64? LUSysInterfacelanguageKey { get; set; }
        public string LUSysInterfacelanguageName { get; set; }
        public Int64? LUCountryKey { get; set; }
        public string LUCountryName { get; set; }
    }
}
