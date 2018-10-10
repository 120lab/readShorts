using System;

namespace readShorts.Entities.dbo
{
    /// <summary>
    /// Entity use : Save all actions on app include errors
    /// </summary>
    public class Ad : EntityBase
    {
        public virtual string CompanyName { get; set; }
        public virtual int Interval { get; set; }
        public virtual string AdPath { get; set; }
        public virtual string AdBody { get; set; }

        /// FK
        public Int64? LUShortAgeRestrictionKey { get; set; }
        public Int64? LUSysInterfacelanguageKey { get; set; }
        public Int64? LUCountryKey { get; set; }
    }
}
