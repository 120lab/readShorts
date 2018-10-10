using readShorts.Models.ViewModels;

namespace readShorts.Services.Interfaces
{
    public interface ILookupService : IServiceBase
    {
        LookupViewModel Get(string tableName);
    }
}
