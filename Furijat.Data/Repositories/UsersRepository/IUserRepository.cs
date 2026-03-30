using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Models;

namespace Furijat.Data.Repositories.UsersRepository;

public interface IUserRepository
{
    public Task<UserResponseDTO> GetUserAsync(string userId);

    public Task<User> GetUserByNameAsync(string username);

    public Task<bool> CheckUserExistsAsync(string? id, string? userName, string? email);

    public Task<List<UserResponseDTO>> GetUersAsync();

    public Task<string> AddUserAsync(RegisterRequestDTO newUserRequest, string hashedPassword);

    public Task CreateFolders();

    public Task<bool> UpdateUserAsync(UserResponseDTO usertoupdate);

    public Task<bool> RemoveUserAsync(string userid);
}