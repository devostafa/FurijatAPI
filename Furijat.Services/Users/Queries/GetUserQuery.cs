using Furijat.Data.DTOs;
using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Services.Base.Queries;

namespace Furijat.Services.Users.Queries;

public class GetUserQuery(string userId) : IQuery<UserResponseDTO>
{
    public string UserID { get; set; } = userId;
}