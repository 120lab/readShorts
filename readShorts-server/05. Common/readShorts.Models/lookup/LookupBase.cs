using System;

namespace readShorts.Models.LOOKUP
{
    //[Serializable]
    public class LookupBase : ModelBase
    {
        /// <summary>
        /// for serialization
        /// </summary>
        //
        public LookupBase()
        {
        }

        public string Description { get; set; }
        public Int64? LUSysInterfacelanguageKey { get; set; }
        public string AditionalData { get; set; }
    }
}