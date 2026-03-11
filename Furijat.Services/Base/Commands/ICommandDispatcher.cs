namespace Furijat.Services.Base.Commands;

public interface ICommandDispatcher
{
    Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken ct);
}