namespace CoBudget.Domain.Repositories.User;

public interface IUserReadRepository
{
    Task<bool> ExistsActiveUserWithEmail(string email);
}
