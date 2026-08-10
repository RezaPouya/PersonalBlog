namespace AppServices.Base
{
    public interface ICommandHandler<CommandAbstract, TResult>
    {
        Task<TResult> Invoke(CommandAbstract request, CancellationToken cancellationToken);
    }
}
