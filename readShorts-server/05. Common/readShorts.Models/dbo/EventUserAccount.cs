namespace readShorts.Models.dbo
{
    using System;

    public partial class EventUserAccount : DboBase
    {
        public string AdditionalData { get; set; }

        /// FK
        public Int64 UserAccountKey { get; set; }

        public Int64 LUEventTypeKey { get; set; }
    }
}