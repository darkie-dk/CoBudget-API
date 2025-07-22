using CoBudget.Domain.Entities;

namespace CoBudget.Domain.Repositories.Users;
public interface IUserWriteRepository
{
    Task Add(User user);
}
