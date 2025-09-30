using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Modular.Modules.Users.Data.Contexts;

namespace Modular.Modules.Users.Services.MigrationService;

/// <summary>
///     <see cref="BackgroundService" /> implementation for running database migrations.
/// </summary>
/// <param name="serviceProvider"><see cref="IServiceProvider" /> implementation to access DI services.</param>
/// <param name="hostApplicationLifetime"><see cref="IHostApplicationLifetime" /> in order to kill the process.</param>
internal sealed class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using Activity? activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();
            await using UsersDbContext dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

            await RunMigrationsAsync(dbContext, stoppingToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    /// <summary>
    ///     Runs any pending migrations on the database.
    /// </summary>
    /// <param name="dbContext"><see cref="UsersDbContext" /> instance for database/persistence interaction.</param>
    /// <param name="cancellationToken">Propagates notification that processes should be canceled.</param>
    /// <returns>A <see cref="Task" /> which resembles the asynchronous operation.</returns>
    private static Task RunMigrationsAsync(UsersDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
        return strategy.ExecuteAsync(dbContext, (context, ctx) => context.Database.MigrateAsync(ctx),
            cancellationToken);
    }
}