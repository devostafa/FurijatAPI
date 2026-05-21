using Furijat.Data.DTOs.RequestDTO;
using Furijat.Services.Base.Commands;

namespace Furijat.Services.Users.Commands;

public record RegisterUserCommand(RegisterRequestDTO RegisterRequest) : ICommand<bool>;