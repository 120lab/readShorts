using readShorts.DataAccess.Interfaces;

namespace readShorts.DataAccess.Repositories
{
    using Framework.Core;

    public sealed class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private DataContext dataContext;

        public DataContext Get()
        {
            return dataContext ?? (dataContext = new DataContext());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
