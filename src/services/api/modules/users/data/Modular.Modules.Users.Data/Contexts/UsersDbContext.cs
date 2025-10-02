using Microsoft.EntityFrameworkCore;

using Modular.Common.Domain.Abstractions;
using Modular.Common.Infrastructure.Data;
using Modular.Modules.Users.Data.Users;
using Modular.Modules.Users.Domain;

namespace Modular.Modules.Users.Data.Contexts;

/// <summary>
///     <see cref="DbContext" /> implementation for the User module for persistence interaction.
/// </summary>
/// <param name="options"><see cref="DbContextOptions{TContext}" /> instance with configuration.</param>
public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : ModularContext(options), IUnitOfWork
{
    public DbSet<User> Users => Set<User>();

    /// <inheritdoc />
    protected override void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}