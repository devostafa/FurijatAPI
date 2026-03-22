using Furijat.Data.DTOs;
using Furijat.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Furijat.Data.Repositories.UsersRepository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnv;

    public UserRepository(DataContext db, IMapper mapper, IWebHostEnvironment webHostEnv)
    {
        _mapper = mapper;
        _db = db;
        _webHostEnv = webHostEnv;
    }

    public async Task<UserDTO> GetUserAsync(string userid)
    {
        return await _db.Users.Include(u => u.Project).ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
            .FirstAsync(u => u.Id == Guid.Parse(userid));
    }

    public async Task<User> GetUserByNameAsync(string username)
    {
        return await _db.Users.FirstAsync(u => u.Name == username);
    }

    public async Task<bool> CheckUserExistsAsync(string username)
    {
        return await _db.Users.AnyAsync(u => u.Name == username);
    }

    public async Task<List<UserDTO>> GetUersAsync()
    {
        return await _db.Users.Include(u => u.Project).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<bool> AddUserAsync(UserToAddDTO newUser, string hashedPassword)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateUserAsync(UserDTO usertoupdate)
    {
        var query = await _db.Users.Include(u => u.Project).FirstAsync(u => u.Id == usertoupdate.Id);
        query = _mapper.Map<User>(usertoupdate);
        _db.Users.Update(query);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveUserAsync(string userid)
    {
        var query = await _db.Users.FindAsync(Guid.Parse(userid));

        if (query != null)
        {
            _db.Users.Remove(query);
            await _db.SaveChangesAsync();
            return true;
        }

        return true;
    }

    public async Task CreateFolders()
    {
        try
        {
            List<User> query = await _db.Users.ToListAsync();

            foreach (var user in query)
            {
                var newUserFolder = Path.Combine(_webHostEnv.ContentRootPath, "Storage", "Users",
                    $"{user.Id}", "Images");
                Directory.CreateDirectory(newUserFolder);
            }

            Console.WriteLine("Created users assets folders successfully");
        }
        catch (Exception err)
        {
            throw err;
        }
    }

    public async Task<User> GetUserDirect(string userid)
    {
        return await _db.Users.FirstAsync(u => u.Id == Guid.Parse(userid));
    }

    public async Task<bool> AddUser(UserToAddDTO usertoadd)
    {
        var newUser = _mapper.Map<User>(usertoadd);
        await _db.Users.AddAsync(newUser);
        await _db.SaveChangesAsync();
        return true;
    }
}