namespace Framework.Core.Interfaces.CQRS
{
    /// <summary>
    /// Base interface for command handlers
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public interface ICommandHandler<in TParameter, out TResult> where TParameter : ICommand where TResult : IQueryResult
    {
        /// <summary>
        /// Executes a command handler
        /// </summary>
        /// <param name="command">The command to be used</param>
        TResult Execute(TParameter command);
    }

}
