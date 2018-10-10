using System;

namespace readShorts.Models.dbo
{
    public class Audit : DboBase
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual string ProcessId { get; set; }
        public virtual string TypeName { get; set; }
        public virtual int? Location { get; set; }
        public virtual string Body { get; set; }
    }
}
