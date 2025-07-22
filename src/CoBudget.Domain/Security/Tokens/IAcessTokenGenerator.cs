using CoBudget.Domain.Entities;

namespace CoBudget.Domain.Security.Tokens;
public interface IAcessTokenGenerator
{
    string GenerateToken(User user);
}
