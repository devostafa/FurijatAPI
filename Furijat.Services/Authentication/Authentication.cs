using AutoMapper;
using Furijat.Data.DTOs;
using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Data.Services.PasswordHash;
using Furijat.Services.Jwt;
using Furijat.Services.Jwt.DTO;
using Furijat.Services.Mail;

namespace Furijat.Services.Authentication;

public class Authentication : IAuthentication
{
    private readonly IJWT _jwtService;
    private readonly IMail _mailService;
    private readonly IMapper _mapper;
    private readonly IPasswordHash _passwordHashService;
    private readonly IUserRepository _usersRepo;

    public Authentication(IJWT jwtService, IMapper mapper, IPasswordHash passwordHashService, IUserRepository usersRepo, IMail mailService)
    {
        _jwtService = jwtService;
        _mapper = mapper;
        _passwordHashService = passwordHashService;
        _usersRepo = usersRepo;
        _mailService = mailService;
    }

    public async Task<string> LoginAsync(LoginRequestDTO loginreq)
    {
        var token = "";

        var checkuser = await _usersRepo.CheckUserExistsAsync(loginreq.Username);

        if (!checkuser)
        {
            return "username / password are wrong";
        }

        var user = await _usersRepo.GetUserByNameAsync(loginreq.Username);

        var jwtRequest = new JWTRequestDTO(user.Id.ToString(), user.Usertype);

        var verifyPasswordResult = await VerifyPassword(loginreq.Password, user.Hashedpassword);

        if (verifyPasswordResult)
        {
            token = _jwtService.CreateToken(jwtRequest);
            return token;
        }

        return "username / password are wrong";
    }


    public async Task<bool> RegisterAsync(RegisterRequestDTO registerreq)
    {
        if (await CheckUser(registerreq.Name))
        {
            return false;
        }


        var newUserDto = _mapper.Map<NewUserRequestDTO>(registerreq);

        var hashedpassword = _passwordHashService.CreateHashedPassword(registerreq.Password);


        var successfulAdd = await _usersRepo.AddUserAsync(newUserDto, hashedpassword);

        if (successfulAdd)
        {
            await MailSuccessfulRegistration(registerreq);
            return true;
        }

        return false;
    }

    private async Task<bool> VerifyPassword(string passwordtoverify, string hashedpassword)
    {
        //1-extract Salt from database user hashedpassword, pass string pattern SALT.HASHEDPASSWORD
        var extractedsavedpassword = hashedpassword.Split(".");
        var extractedsalt = extractedsavedpassword[0];
        var extractedhashedpass = extractedsavedpassword[1];
        //2-generate hashed password with given Salt
        var passwordtotest = _passwordHashService.HashPasswordWithGivenSalt(extractedsalt, passwordtoverify);

        //3-compare
        if (passwordtotest == extractedhashedpass)
        {
            return true;
        }

        return false;
    }

    private async Task<bool> CheckUser(string username)
    {
        return await _usersRepo.CheckUserExistsAsync(username);
    }

    private async Task MailSuccessfulRegistration(RegisterRequestDTO registerRequest)
    {
        var successfulRegistrationNotify = new MailRequestDTO
        {
            Emailto = registerRequest.Email,
            Subject = "Welcome to FundHub",
            Message = $"Dear {registerRequest.Name}, thank you for registering on FundHub\\n" +
                      $"We hope you enjoy the platform and support your community of people who wish to achieve their dreams and ideas"
        };
        await _mailService.SendMail(successfulRegistrationNotify);
    }
}