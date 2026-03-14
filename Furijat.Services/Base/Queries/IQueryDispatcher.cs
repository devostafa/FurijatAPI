namespace Furijat.Services.Base.Queries;

public interface IQueryDispatcher
{
    Task<TResult> Query<TResult>(IQuery<TResult> query, CancellationToken ct = default);
}