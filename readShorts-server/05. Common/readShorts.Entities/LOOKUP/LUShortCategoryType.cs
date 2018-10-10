namespace readShorts.Entities.LOOKUP
{
    public partial class LUShortCategoryType : LookupBase
    {
        public LUShortCategoryType()
        {
        }

        public LUShortCategoryType(LookupBase bs)
        {
            this.Description = bs.Description;
            this.LUSysInterfacelanguageKey = bs.LUSysInterfacelanguageKey;
        }
    }
}