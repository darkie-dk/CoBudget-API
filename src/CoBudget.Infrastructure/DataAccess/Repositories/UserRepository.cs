using CoBudget.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CoBudget.Infrastructure.DataAccess.Repositories;

internal class UserRepository(CoBudgetDbContext context) : IUserReadRepository
{
    private readonly CoBudgetDbContext _context = context;

    public async Task<bool> ExistsActiveUserWithEmail(string email)
    {
        return await _context.Users.AnyAsync(user => user.Email == email);
    }
}
