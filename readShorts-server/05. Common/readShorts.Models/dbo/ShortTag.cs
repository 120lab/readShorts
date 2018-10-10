namespace readShorts.Models.dbo
{
    using System;

    public partial class ShortTag : DboBase
    {

        /// FK
        public Int64 LUShortTagTypeKey { get; set; }
        public string LUShortTagTypeName { get; set; }
        public Int64 ShortKey { get; set; }
    }
}
