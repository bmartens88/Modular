using Microsoft.EntityFrameworkCore;

using Modular.Common.Infrastructure.Outbox;

namespace Modular.Common.Infrastructure.Data;

/// <summary>
///     Serves as a base implementation of <see cref="DbContext" /> which configures <see cref="OutboxMessage" /> entities.
/// </summary>
/// <param name="options"><see cref="DbContextOptions" /> instance containing configuration data.</param>
public abstract class ModularContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ApplyConfigurations(modelBuilder);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    ///     The implementing context should override this method to apply its own configurations.
    /// </summary>
    /// <param name="modelBuilder"><see cref="ModelBuilder" /> instance for applying configuration.</param>
    protected abstract void ApplyConfigurations(ModelBuilder modelBuilder);
}