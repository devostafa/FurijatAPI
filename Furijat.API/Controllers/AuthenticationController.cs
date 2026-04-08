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
    public async Task<string?> Login(LoginRequestDTO loginReq)
    {
        return await _auth.LoginAsync(loginReq);
    }

    [HttpPost("register")]
    public async Task<bool> Register(RegisterRequestDTO registerReq)
    {
        return await _auth.RegisterAsync(registerReq);
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<UserResponseDTO?> GetUserInfo()
    {
        var userId = HttpContext.User.FindFirst("userId").Value;

        if (userId == null) return null;

        return await _useRepo.GetUserAsync(userId);
    }
}