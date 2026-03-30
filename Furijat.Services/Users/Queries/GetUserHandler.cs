using Furijat.Data.DTOs;
using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Repositories.UsersRepository;
using Furijat.Services.Base.Queries;

namespace Furijat.Services.Users.Queries;

public class GetUserHandler : IQueryHandler<GetUserQuery, UserResponseDTO>
{
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDTO> HandleAsync(GetUserQuery query, CancellationToken ct = default)
    {
        var userResult = await _userRepository.GetUserAsync(query.UserID);

        return userResult;
    }
}