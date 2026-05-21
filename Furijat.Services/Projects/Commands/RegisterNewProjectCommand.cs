using Furijat.Data.DTOs.RequestDTO;
using Furijat.Services.Base.Commands;

namespace Furijat.Services.Projects.Commands;

public record RegisterNewProjectCommand(ProjectRequestDTO newProjectRequest) : ICommand<string>;