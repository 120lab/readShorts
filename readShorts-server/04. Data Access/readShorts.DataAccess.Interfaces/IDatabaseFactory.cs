using System;

namespace readShorts.DataAccess.Interfaces
{
    public interface IDatabaseFactory : IDisposable
    {
        DataContext Get();
    }
}
