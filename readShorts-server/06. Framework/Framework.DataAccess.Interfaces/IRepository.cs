using System;
using System.Linq.Expressions;

namespace Framework.DataAccess.Interfaces
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : class
    {     

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> where);
        
    }

}
