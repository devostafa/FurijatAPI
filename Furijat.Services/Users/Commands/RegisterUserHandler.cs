using Furijat.Data.Models;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Data.Services.PasswordHash;
using Furijat.Services.Base.Commands;
using Furijat.Services.Mail;

namespace Furijat.Services.Users.Commands;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand, bool>
{
    private readonly IMail _mailService;
    private readonly IPasswordHash _passwordHashService;
    private readonly IUserRepository _userRepository;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordHash passwordHashService, IMail mailService)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
        _mailService = mailService;
    }

    public async Task<bool> HandleAsync(RegisterUserCommand command, CancellationToken ct = default)
    {
        var hashedPassword = _passwordHashService.CreateHashedPassword(command.RegisterRequest.Password);

        var checkAdd = await _userRepository.AddUser(command.RegisterRequest, hashedPassword);

        var user = await _userRepository.GetUser(newUser.Id);

        var mailRequest = new MailRequest
        {
            Emailto = user.Email,
            Subject = "Registration Successful",
            Message = $"Welcome to Furijat. Dear {user.Name}, thank you for registering"
        };

        var checkEmail = await _mailService.SendMail(mailRequest);

        return true;
    }
}