using Furijat.Data.Data.DTOs;
using Furijat.Data.Data.DTOs.RequestDTO;
using Furijat.Services.Services.Authentication;
using Furijat.Services.Services.JWT;
using Furijat.Services.Services.Repositories.UsersRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;


[Route("Authentication")]
public class AuthenticationController : BaseController
{
    private readonly IAuthentication _auth;
    private readonly IJWT _jwt;
    private readonly IUserRepository _userepo;

    public AuthenticationController(IAuthentication auth, IUserRepository userrepo ,IJWT jwt)
    {
        _auth = auth;
        _jwt = jwt;
        _userepo = userrepo;
    }

    [HttpPost("Login")]
    [Produces("application/json")]
    public async Task<string?> Login(LoginRequestDTO loginrequest)
    {
        return await _auth.Login(loginrequest);
    }

    [HttpGet("CheckToken")]
    [Authorize]
    public async Task<bool> CheckToken()
    {
        return true;
    }

    [HttpPost("Register")]
    public async Task<bool> Register(RegisterRequestDTO registerrequest)
    {
        return await _auth.Register(registerrequest);
    }
    
    [Authorize]
    [HttpGet("GetLoggedUser")]
    public async Task<UserDTO> GetUserInfo()
    {
        string userid = HttpContext.User.FindFirst("userid").Value;
        return await _userepo.GetUser(userid);
    }
    
}