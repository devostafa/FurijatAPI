using Furijat.Data.DTOs.RequestDTO;
using Furijat.Services.Base.Commands;

namespace Furijat.Services.Users.Commands;

public class RegisterUserCommand : ICommand<bool>
{
    public RegisterRequestDTO RegisterRequest { get; set; }
}