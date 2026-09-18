namespace AppServices.Base;

public interface ICommand
{ }

public interface ICommand<TResult>
{ }

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task Invoke(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
}