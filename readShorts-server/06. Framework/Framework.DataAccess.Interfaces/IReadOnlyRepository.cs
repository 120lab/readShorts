using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.DataAccess.Interfaces
{
    public interface IReadOnlyRepository<T> where T : class
    {
        T GetById(int id);

        IQueryable<T> GetAll();

        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
    }

}
