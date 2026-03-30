using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("auth")]
public class AuthenticationController : BaseController
{
    private readonly IAuthentication _auth;
    private readonly IUserRepository _useRepo;

    public AuthenticationController(IAuthentication auth, IUserRepository userRepo)
    {
        _auth = auth;
        _useRepo = userRepo;
    }

    [HttpPost("login")]
    [Produces("application/json")]
    public async Task<string?> Login(LoginRequestDTO loginrequest)
    {
        return await _auth.LoginAsync(loginrequest);
    }

    [HttpPost("register")]
    public async Task<bool> Register(RegisterRequestDTO registerrequest)
    {
        return await _auth.RegisterAsync(registerrequest);
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<UserResponseDTO?> GetUserInfo()
    {
        var userId = HttpContext.User.FindFirst("userid").Value;

        if (userId == null) return null;

        return await _useRepo.GetUserAsync(userId);
    }
}