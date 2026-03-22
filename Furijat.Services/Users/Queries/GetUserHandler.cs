using Furijat.Data.DTOs;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Services.Base.Queries;

namespace Furijat.Services.Users.Queries;

public class GetUserHandler : IQueryHandler<GetUserQuery, UserDTO>
{
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDTO> HandleAsync(GetUserQuery query, CancellationToken ct = default)
    {
        var userResult = await _userRepository.GetUserAsync(query.UserID);

        return userResult;
    }
}