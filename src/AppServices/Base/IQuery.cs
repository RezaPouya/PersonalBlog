namespace AppServices.Base;

public interface IQuery<TResult>
{ }

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> Invoke(TQuery query, CancellationToken cancellationToken);
}