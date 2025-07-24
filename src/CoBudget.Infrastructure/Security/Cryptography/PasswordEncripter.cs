using CoBudget.Domain.Security.Cryptography;
using BCrypter = BCrypt.Net.BCrypt;

namespace CoBudget.Infrastructure.Security.Cryptography;

public class PasswordEncripter : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        return BCrypter.HashPassword(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BCrypter.Verify(password, hashedPassword);
    }
}
