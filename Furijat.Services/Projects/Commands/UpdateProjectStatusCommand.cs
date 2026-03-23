using Furijat.Data.Enums;
using Furijat.Services.Base.Commands;

namespace Furijat.Services.Projects.Commands;

public class UpdateProjectStatusCommand : ICommand<bool>
{
    public required string ProjectId { get; set; }
    public ProjectStatusEnum Status { get; set; }
}