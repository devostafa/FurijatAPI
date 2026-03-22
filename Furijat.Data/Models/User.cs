using System.ComponentModel.DataAnnotations;

namespace Furijat.Data.Models;

public class User
{
    [Key] public Guid Id { get; set; }
    public string Name { get; set; }
    public string Hashedpassword { get; set; }
    public string Usertype { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public string? Facebook { get; set; }
    public string? X { get; set; }
    public string? Instagram { get; set; }
    public string Profileimage { get; set; }

    public Project? Project { get; set; } // For now just 1 project per user
    // public IList<Donation> Donations { get; set; }
}