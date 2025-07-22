using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CoBudget.Infrastructure.DataAccess.Repositories;

internal class UserRepository(CoBudgetDbContext context) : IUserReadRepository, IUserWriteRepository
{
    private readonly CoBudgetDbContext _context = context;

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<bool> ExistsActiveUserWithEmail(string email)
    {
        return await _context.Users.AnyAsync(user => user.Email == email);
    }

    
}
