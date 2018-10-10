using System;

namespace readShorts.Models.dbo
{
    //[Serializable]
    public class DboBase : ModelBase
    {
        /// <summary>
        /// for serialization
        /// </summary>
        protected DboBase()
        {
        }

        public DateTime CreatedTimeStamp { get; set; }
        public DateTime LastUpdateTimeStamp { get; set; }
        public string UniqueGuid { get; set; }
    }
}