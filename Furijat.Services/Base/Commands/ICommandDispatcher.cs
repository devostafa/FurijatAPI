namespace Furijat.Services.Base.Commands;

public interface ICommandDispatcher
{
    Task<TResult> Dispatch<TResult>(ICommand<TResult> command, CancellationToken ct = default);
}