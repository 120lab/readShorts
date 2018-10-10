using System;

namespace readShorts.Entities.dbo
{
    /// <summary>
    /// Entity use : Save all actions on app include errors
    /// </summary>
    public class Audit : EntityBase
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual string ProcessId { get; set; }
        public virtual string TypeName { get; set; }
        public virtual int? Location { get; set; }
        public virtual string Body { get; set; }
  
    }
}
