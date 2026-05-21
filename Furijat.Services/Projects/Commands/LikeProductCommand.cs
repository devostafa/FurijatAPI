using Furijat.Services.Base.Commands;

namespace Furijat.Services.Projects.Commands;

public record LikeProductCommand(string ProductId) : ICommand<bool>;