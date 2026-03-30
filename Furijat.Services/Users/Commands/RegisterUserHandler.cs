using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.Enums;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Services.Base.Commands;
using Furijat.Services.Mail;
using Furijat.Services.PasswordHash;

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

        var newUserId = await _userRepository.AddUserAsync(command.RegisterRequest, hashedPassword);

        var user = await _userRepository.GetUserAsync(newUserId);

        var mailRequest = new MailRequestDTO
        {
            MailType = MailRequestTypeEnum.NewUserRegistered,
            Emailto = user.Email,
            Subject = "Registration Successful",
            Message = $"Welcome to Furijat. Dear {user.Name}, thank you for registering"
        };

        var emailResult = await _mailService.SendMailAsync(mailRequest);

        return emailResult;
    }
}