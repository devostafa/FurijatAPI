using AutoMapper;
using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.Enums;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Services.Jwt;
using Furijat.Services.Jwt.DTO;
using Furijat.Services.Mail;
using Furijat.Services.PasswordHash;

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

    public async Task<string?> LoginAsync(LoginRequestDTO loginreq)
    {
        var token = "";

        var checkuser = await _usersRepo.CheckUserExistsAsync(null, null, loginreq.Email);

        if (!checkuser)
        {
            return null;
        }

        var user = await _usersRepo.GetUserByNameAsync(loginreq.Email);

        var jwtRequest = new JWTRequestDTO(user.Id.ToString(), user.Usertype);

        var verifyPasswordResult = await VerifyPassword(loginreq.Password, user.Hashedpassword);

        if (verifyPasswordResult)
        {
            return _jwtService.CreateToken(jwtRequest);
        }

        return null;
    }


    public async Task<bool> RegisterAsync(RegisterRequestDTO registerReq)
    {
        var checkResult = await _usersRepo.CheckUserExistsAsync(null, registerReq.Email, null);

        var hashedPassword = _passwordHashService.CreateHashedPassword(registerReq.Password);

        var result = await _usersRepo.AddUserAsync(registerReq, hashedPassword);

        var user = await _usersRepo.GetUserAsync(result);

        if (user != null)
        {
            var mailReq = new MailRequestDTO(MailRequestTypeEnum.NewUserRegistered, user.Email, "Registration Successful",
                $"Welcome to Furijat. Dear {user.Name}, thank you for registering", null, null, null);
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
}