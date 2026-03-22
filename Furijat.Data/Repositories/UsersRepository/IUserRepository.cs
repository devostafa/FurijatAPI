using Furijat.Data.DTOs;
using Furijat.Data.Models;

namespace Furijat.Data.Repositories.UsersRepository;

public interface IUserRepository
{
    public Task<UserDTO> GetUserAsync(string userid);

    public Task<User> GetUserByNameAsync(string username);

    public Task<bool> CheckUserExistsAsync(string username);

    public Task<List<UserDTO>> GetUersAsync();

    public Task<bool> AddUserAsync(UserToAddDTO newUser, string hashedPassword);

    public Task CreateFolders();

    public Task<bool> UpdateUserAsync(UserDTO usertoupdate);

    public Task<bool> RemoveUserAsync(string userid);
}