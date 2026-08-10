namespace AppServices.Base
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> Invoke(TQuery input, CancellationToken cancellationToken);
    }
}
