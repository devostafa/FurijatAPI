using Furijat.Data.Data.DTOs;
using Furijat.Data.Data.Models;

namespace Furijat.Services.Services.Repositories.UsersRepository;

public interface IUserRepository
{
    public Task<UserDTO> GetUser(string userid);
    public Task<User> GetUserByName(string username);
    public Task<bool> CheckUser(string username);
    public Task<User> GetUserDirect(string userid);
    public Task<List<UserDTO>> GetUers();
    public Task<bool> AddUser(UserToAddDTO usertoadd);
    public Task CreateFolders();
    public Task<bool> UpdateUser(UserDTO usertoupdate);
    public Task<bool> RemoveUser(string userid);
}