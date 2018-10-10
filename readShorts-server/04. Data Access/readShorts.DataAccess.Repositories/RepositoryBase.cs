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
    public abstract class RepositoryBase<T> : ReadOnlyRepositoryBase<T>, IRepository<T>
        where T : EntityBase, new()
    {
        protected RepositoryBase(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public virtual T Add(T entity)
        {
            UpdateDefaults(entity, true);
            dbset.Add(entity);
            return entity;
        }

        public virtual void Update(T entity)
        {
            var local = dataContext.Set<T>().Local.FirstOrDefault(f => f.RecordKey == entity.RecordKey);
            if (local != null)
            {
                dataContext.Entry(local).State = EntityState.Detached;
            }
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
            UpdateDefaults(entity, false);
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
            //entity.IsRowDeleted = true;
            // Update(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var entities = dbset.Where<T>(where);

            Parallel.ForEach(entities, entity => Delete(entity));
        }

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