using Microsoft.Extensions.DependencyInjection;

namespace Furijat.Services.Base.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken ct = default)
    {
        ICommandHandler<ICommand<TResult>, TResult> handler = _serviceProvider.GetRequiredService<ICommandHandler<ICommand<TResult>, TResult>>();

        return await handler.HandleAsync(command, ct);
    }
}