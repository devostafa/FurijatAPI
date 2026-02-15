using System.ComponentModel.DataAnnotations;

namespace Furijat.Data.Data.Models;

public class Project
{

    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int Currentfund { get; set; }
    public int Totalfundrequired { get; set; }
    public string[] Imagesnames { get; set; }
    
    public IList<Donation> Donations { get; set; }
    public string Facebook { get; set; }
    public string X { get; set; }
    public string Instagram { get; set; }


}