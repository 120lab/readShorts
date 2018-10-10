using Framework.DataAccess.Interfaces;
using readShorts.DataAccess.Interfaces;
using readShorts.Entities;
using readShorts.Entities.LOOKUP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace readShorts.DataAccess.Repositories
{
    public abstract class RepositoryLookupBase<T> : ReadOnlyRepositoryBase<T>, IRepository<T>
        where T : LookupBase, new()
    {
        protected RepositoryLookupBase(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public virtual T Add(T entity)
        {
            UpdateDefaults(entity, true);
            dbset.Add(entity);
            return entity;
        }

        public void Update(T entity)
        { }

        public void Delete(T entity)
        { }

        public void Delete(Expression<Func<T, bool>> where)
        { }

        private void UpdateDefaults(T entity, bool isNew)
        {
            if (isNew)
            {
                PropertyInfo createdDateInfo = entity.GetType().GetProperty("CreatedTimeStamp");
                if (createdDateInfo != null)
                {
                    createdDateInfo.SetValue(entity, DateTime.UtcNow);
                }
            }
            else
            {
                dataContext.Entry(entity).Property("CreatedTimeStamp").IsModified = false;
            }

            PropertyInfo lastUpdateDateInfo = entity.GetType().GetProperty("LastUpdateTimeStamp");
            if (lastUpdateDateInfo != null)
            {
                lastUpdateDateInfo.SetValue(entity, DateTime.UtcNow);
            }
        }
    }
}