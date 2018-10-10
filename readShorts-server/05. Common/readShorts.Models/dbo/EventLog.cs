namespace readShorts.Models.dbo
{
    using System;

    public partial class EventLog : DboBase
    {
        public Guid Id { get; set; }

        public int EventTypeCode { get; set; }

        public string Description { get; set; }
        
    }
}
