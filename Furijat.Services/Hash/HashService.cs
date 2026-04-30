using System.Security.Cryptography;
using System.Text;

namespace Furijat.Services.PasswordHash;

public class HashService : IHashService
{
    public string CreateHashedPassword(string password)
    {
        var salt = GenerateSalt();
        var hashedpassword = GenerateHashedPassword(salt, password);
        //create a string of pattern [SALT.HASHEDPASSWORD]
        var hashedpass = salt + "." + hashedpassword;
        return hashedpass;
    }

    public string HashPasswordWithGivenSalt(string salt, string password)
    {
        return GenerateHashedPassword(salt, password);
    }

    private string GenerateSalt()
    {
        var salt = new byte[16];
        var rng = new RNGCryptoServiceProvider();
        rng.GetBytes(salt);
        var base64salt = Convert.ToBase64String(salt);
        return base64salt;
    }

    private string GenerateHashedPassword(string salt, string password)
    {
        var passwordbytes = Encoding.UTF8.GetBytes(password);
        var saltbytes = Convert.FromBase64String(salt);

        var combinedbytes = new byte[saltbytes.Length + passwordbytes.Length];
        Buffer.BlockCopy(saltbytes, 0, combinedbytes, 0, saltbytes.Length);
        Buffer.BlockCopy(passwordbytes, 0, combinedbytes, saltbytes.Length, passwordbytes.Length);

        var sha256 = SHA256.Create();
        var hashedbytes = sha256.ComputeHash(combinedbytes);
        var hashedpassword = Convert.ToBase64String(hashedbytes);
        return hashedpassword;
    }
}