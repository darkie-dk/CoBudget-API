namespace CoBudget.Domain.Repositories.Users;

public interface IUserReadRepository
{
    Task<bool> ExistsActiveUserWithEmail(string email);
}
