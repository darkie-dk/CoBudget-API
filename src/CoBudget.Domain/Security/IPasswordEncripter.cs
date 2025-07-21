namespace CoBudget.Domain.Security;

public interface IPasswordEncripter
{
    string Encrypt(string password);
}
