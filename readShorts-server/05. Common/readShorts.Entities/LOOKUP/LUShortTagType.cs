namespace readShorts.Entities.LOOKUP
{
    public partial class LUShortTagType : LookupBase
    {
        public LUShortTagType()
        {
        }

        public LUShortTagType(LookupBase bs)
        {
            this.Description = bs.Description;
            this.LUSysInterfacelanguageKey = bs.LUSysInterfacelanguageKey;
        }
    }
}