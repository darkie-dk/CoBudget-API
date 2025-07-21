using CoBudget.Domain.Security;
using BCrypter = BCrypt.Net.BCrypt;

namespace CoBudget.Infrastructure.Security;

public class PasswordEncripter : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        return BCrypter.HashPassword(password);
    }
}
