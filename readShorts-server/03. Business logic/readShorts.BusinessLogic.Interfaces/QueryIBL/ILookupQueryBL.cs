namespace readShorts.BusinessLogic.Interfaces
{
    using Models.ViewModels;
    using System;

    public interface ILookupQueryBL : IBaseBL
    {

        LookupViewModel Get(string tableName, Int64 lUSysInterfaceLanguageKey);

        LookupViewModel Get(string tableName, Int64 lUSysInterfaceLanguageKey, bool useCache);
    }
}
