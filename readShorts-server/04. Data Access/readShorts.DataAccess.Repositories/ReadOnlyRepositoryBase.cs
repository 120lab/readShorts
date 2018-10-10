using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

using readShorts.DataAccess.Interfaces;
using Framework.DataAccess.Interfaces;

namespace readShorts.DataAccess.Repositories
{

    public abstract class ReadOnlyRepositoryBase<T> : IReadOnlyRepository<T> where T : class
    {
        protected internal DataContext dataContext;
        protected internal readonly IDbSet<T> dbset;

        protected ReadOnlyRepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get; private set;
        }

        protected internal DataContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
        
        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbset;
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }
        
    }
}
