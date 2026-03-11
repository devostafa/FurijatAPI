namespace Furijat.Services.Base.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}