using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Modular.Common.Infrastructure.Data;

namespace Modular.Common.Infrastructure;

/// <summary>
///     Defines extension methods for dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Adds all infrastructure services to the DI container.
    /// </summary>
    /// <param name="services">The DI container for registering infrastructure services.</param>
    /// <returns><see cref="IServiceCollection" /> with the infrastructure services registered.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.TryAddSingleton<DomainEventsInterceptor>();

        return services;
    }
}