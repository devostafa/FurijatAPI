using Furijat.Data.Enums;

namespace Furijat.Data.Models;

public class SocialMedia
{
    public int Id { get; set; }
    public SocialMediaPlatformEnum Platform { get; set; }
    public string Url { get; set; }
    public int ProjectId { get; set; }
}