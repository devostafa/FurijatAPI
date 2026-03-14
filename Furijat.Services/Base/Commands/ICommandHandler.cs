namespace Furijat.Services.Base.Commands;

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    public Task<TResult> HandleAsync(TCommand command, CancellationToken ct = default);
}