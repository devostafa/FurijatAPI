using Furijat.Data.DTOs.RequestDTO;
using Furijat.Services.Base.Commands;
using Furijat.Services.Base.Queries;
using Furijat.Services.Users.Commands;
using Furijat.Services.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("auth")]
public class AuthenticationController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) : BaseController
{

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDTO loginReq)
    {
        var command = new LoginUserCommand(loginReq);

        var result = await commandDispatcher.DispatchAsync(command);

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDTO registerReq)
    {
        var command = new RegisterUserCommand(registerReq);

        var result = await commandDispatcher.DispatchAsync(command);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = HttpContext.User.FindFirst("userId").Value;

        if (userId == null) return BadRequest("User Id not found");

        var query = new GetUserQuery(userId);

        var result = await queryDispatcher.QueryAsync(query);

        return Ok(result);
    }
}