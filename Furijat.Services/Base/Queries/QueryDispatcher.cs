namespace Furijat.Services.Base.Queries;

public class QueryDispatcher : IQueryDispatcher
{
    public async Task<TResult> Query<TResult>(IQuery<TResult> query, CancellationToken ct)
    {
        
    }
}