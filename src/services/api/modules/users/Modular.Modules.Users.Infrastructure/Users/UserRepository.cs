using Microsoft.EntityFrameworkCore;

using Modular.Modules.Users.Data.Contexts;
using Modular.Modules.Users.Domain;
using Modular.Modules.Users.Domain.Abstractions;
using Modular.Modules.Users.Domain.ValueObjects;

namespace Modular.Modules.Users.Infrastructure.Users;

/// <summary>
///     <see cref="IUserRepository" /> implementation.
/// </summary>
/// <param name="dbContext"><see cref="UsersDbContext" /> instance used for persistence interaction.</param>
internal sealed class UserRepository(UsersDbContext dbContext) : IUserRepository
{
    /// <inheritdoc />
    public Task<User?> FindByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return dbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id), cancellationToken);
    }

    /// <inheritdoc />
    public void Add(User user)
    {
        dbContext.Users.Add(user);
    }
}