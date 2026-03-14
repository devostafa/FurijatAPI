using Furijat.Data.DTOs;
using Furijat.Data.Models;

namespace Furijat.Data.Repositories.UsersRepository;

public interface IUserRepository
{
    public Task<UserDTO> GetUser(string userid);

    public Task<User> GetUserByName(string username);

    public Task<bool> CheckUser(string username);

    public Task<List<UserDTO>> GetUers();

    public Task<bool> AddUser(UserToAddDTO newUser, string hashedPassword);

    public Task CreateFolders();

    public Task<bool> UpdateUser(UserDTO usertoupdate);

    public Task<bool> RemoveUser(string userid);
}