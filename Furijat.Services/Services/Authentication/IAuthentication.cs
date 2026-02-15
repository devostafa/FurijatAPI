using Furijat.Data.Data.DTOs.RequestDTO;

namespace Furijat.Services.Services.Authentication;

public interface IAuthentication
{
    public Task<string> Login(LoginRequestDTO loginreq);
    public Task<bool> Register(RegisterRequestDTO registerreq);
}