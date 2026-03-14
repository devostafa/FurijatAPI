using Furijat.Data.DTOs;
using Furijat.Services.Base.Queries;

namespace Furijat.Services.Users.Queries;

public class GetUserQuery(string userId) : IQuery<UserDTO>
{
    public string UserID { get; set; } = userId;
}