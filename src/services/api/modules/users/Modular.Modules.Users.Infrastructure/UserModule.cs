using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Modular.Common.Domain.Abstractions;
using Modular.Common.Infrastructure.Data;
using Modular.Common.Presentation.Endpoints;
using Modular.Modules.Users.Data.Contexts;
using Modular.Modules.Users.Domain.Abstractions;
using Modular.Modules.Users.Infrastructure.Users;
using Modular.Modules.Users.Presentation;

namespace Modular.Modules.Users.Infrastructure;

/// <summary>
///     Defines extension methods for dependency injection for the User module.
/// </summary>
public static class UserModule
{
    /// <summary>
    ///     Adds all module services to the DI container.
    /// </summary>
    /// <param name="services">The DI container for registering module services.</param>
    /// <param name="configuration">Application configuration for accessing settings.</param>
    /// <returns><see cref="IServiceCollection" /> with the module services registered.</returns>
    public static IServiceCollection AddUserModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddInfrastructure(configuration)
            .AddEndpoints(AssemblyReference.Assembly);
    }

    /// <summary>
    ///     Register internal module services to the DI container.
    /// </summary>
    /// <param name="services">The DI container for registering internal module services.</param>
    /// <param name="configuration">Application configuration for accessing settings.</param>
    /// <returns><see cref="IServiceCollection" /> with the module services registered.</returns>
    private static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContextPool<UsersDbContext>((sp, opts) =>
            opts.UseNpgsql(configuration.GetConnectionString("userDb"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
                .AddInterceptors(sp.GetRequiredService<DomainEventsInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        return services;
    }
}