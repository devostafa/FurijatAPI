using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.Enums;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Services.Base.Commands;
using Furijat.Services.Mail;
using Furijat.Services.PasswordHash;

namespace Furijat.Services.Users.Commands;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand, bool>
{
    private readonly IHashService _hashServiceService;
    private readonly IMailService _mailService;
    private readonly IUserRepository _userRepository;

    public RegisterUserHandler(IUserRepository userRepository, IHashService hashServiceService, IMailService mailService)
    {
        _userRepository = userRepository;
        _hashServiceService = hashServiceService;
        _mailService = mailService;
    }

    public async Task<bool> HandleAsync(RegisterUserCommand command, CancellationToken ct = default)
    {
        var hashedPassword = _hashServiceService.CreateHashedPassword(command.RegisterRequest.Password);

        var newUserId = await _userRepository.AddUserAsync(command.RegisterRequest, hashedPassword);

        var user = await _userRepository.GetUserAsync(newUserId);

        var mailRequest = new MailRequestDTO(
            MailType: MailRequestTypeEnum.NewUserRegistered,
            EmailTo: user.Email,
            Subject: "Registration Successful",
            CustomMessage: $"Welcome to Furijat. Dear {user.Name}, thank you for registering",
            User: user,
            Project: null,
            Donation: null
        );

        var emailResult = await _mailService.SendMailAsync(mailRequest);

        return emailResult;
    }
}