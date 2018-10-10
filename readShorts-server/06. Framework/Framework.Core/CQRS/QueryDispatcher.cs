using Castle.MicroKernel;
using Framework.Core.Interfaces.CQRS;
using System;

namespace Framework.Core.CQRS
{
    public class QueryDispatcher : Disposable, IQueryDispatcher
    {
        private readonly IKernel _kernel;

        public QueryDispatcher(IKernel kernel)
        {
            if (kernel == null)
                throw new ArgumentException("kernel");

            _kernel = kernel;
        }

        public TResult Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var handler = _kernel.Resolve<IQueryHandler<TParameter, TResult>>();
            return handler.Retrieve(query);
        }
    }
}