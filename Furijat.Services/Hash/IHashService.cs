namespace Furijat.Services.PasswordHash;

public interface IHashService
{
    public string CreateHashedPassword(string password);

    public string HashPasswordWithGivenSalt(string salt, string password);
}