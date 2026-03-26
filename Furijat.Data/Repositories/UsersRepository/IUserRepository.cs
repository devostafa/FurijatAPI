using Furijat.Data.DTOs;
using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.Models;

namespace Furijat.Data.Repositories.UsersRepository;

public interface IUserRepository
{
    public Task<UserDTO> GetUserAsync(string userId);

    public Task<User> GetUserByNameAsync(string username);

    public Task<bool> CheckUserExistsAsync(string userName);

    public Task<List<UserDTO>> GetUersAsync();

    public Task<string> AddUserAsync(RegisterRequestDTO newUserRequest, string hashedPassword);

    public Task CreateFolders();

    public Task<bool> UpdateUserAsync(UserDTO usertoupdate);

    public Task<bool> RemoveUserAsync(string userid);
}