using Microsoft.EntityFrameworkCore;
using Account.Domain.Entities;
using Account.Domain.Repositories;
using Account.Infrastructure.Data;

namespace Account.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => u.DeletedAt == null)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken);
    }
    public override async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => u.DeletedAt == null)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => u.DeletedAt == null)
            .FirstOrDefaultAsync(u => u.Username != null && u.Username.ToLower() == username.ToLower(), cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => u.DeletedAt == null)
            .AnyAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => u.DeletedAt == null)
            .AnyAsync(u => u.Username != null && u.Username.ToLower() == username.ToLower(), cancellationToken);
    }
}
