using System;
using Castle.MicroKernel;
using Castle.Windsor;
using Framework.Core.Interfaces.CQRS;

namespace Framework.Core.CQRS
{
    public class CommandDispatcher : Disposable, ICommandDispatcher
    {
        private readonly IKernel _kernel;
          
        public CommandDispatcher(IKernel kernel)
        {
            if (kernel == null)
                throw new ArgumentException("kernel");

            _kernel = kernel;
        }

        public TResult Dispatch<TParameter, TResult>(TParameter command)
            where TParameter : ICommand
            where TResult : IQueryResult

        {
            var handler = _kernel.Resolve<ICommandHandler<TParameter,TResult>>();
            return handler.Execute(command);
        }

    }
}