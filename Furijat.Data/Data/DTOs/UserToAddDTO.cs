using Furijat.Data.Data.Models;

namespace Furijat.Data.Data.DTOs;

public record UserToAddDTO
{

    public Guid Id { get; init; }
    public string Username { get; init; } = "";
    public string Hashedpassword { get; set; } = "";
    public string Usertype { get; init; } = "";
    public int Phonenumber { get; init; } = 0;
    public string Email { get; init; } = "";
    public string Facebook { get; init; } = "";
    public string X { get; init; } = "";
    public string Instagram { get; init; } = "";
    public string Profileimage { get; init; } = "";
    public List<Project> Projects { get; init; }
    public List<Donation> Donations { get; init; }
}