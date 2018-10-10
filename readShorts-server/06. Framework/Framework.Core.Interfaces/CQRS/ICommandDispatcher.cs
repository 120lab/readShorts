namespace Framework.Core.Interfaces.CQRS
{
    /// <summary>
    /// Passed around to all allow dispatching a command and to be mocked by unit tests
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatches a command to its handler
        /// </summary>
        /// <typeparam name="TParameter">Command Type</typeparam>
        /// <param name="command">The command to be passed to the handler</param>
        TResult Dispatch<TParameter, TResult>(TParameter command)
            where TParameter : ICommand
            where TResult : IQueryResult;

        void Dispose();
    }
}
