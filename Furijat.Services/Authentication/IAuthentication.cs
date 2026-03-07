using Furijat.Data.DTOs.RequestDTO;

namespace Furijat.Services.Authentication;

public interface IAuthentication
{
    public Task<string> Login(LoginRequestDTO loginreq);

    public Task<bool> Register(RegisterRequestDTO registerreq);
}