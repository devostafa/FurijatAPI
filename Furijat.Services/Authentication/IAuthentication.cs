using Furijat.Data.DTOs.RequestDTO;

namespace Furijat.Services.Authentication;

public interface IAuthentication
{
    public Task<string> LoginAsync(LoginRequestDTO loginreq);

    public Task<bool> RegisterAsync(RegisterRequestDTO registerreq);
}