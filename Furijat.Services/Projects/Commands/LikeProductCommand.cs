using Furijat.Services.Base.Commands;

namespace Furijat.Services.Projects.Commands;

public class LikeProductCommand : ICommand<bool>
{
    public string ProductId { get; set; }
}