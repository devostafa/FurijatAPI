using Microsoft.Extensions.DependencyInjection;

namespace Furijat.Services.Base.Queries;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> Query<TResult>(IQuery<TResult> query, CancellationToken ct = default)
    {
        IQueryHandler<IQuery<TResult>, TResult> handler = _serviceProvider.GetRequiredService<IQueryHandler<IQuery<TResult>, TResult>>();
        return await handler.HandleAsync(query, ct);
    }
}