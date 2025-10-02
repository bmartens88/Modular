using MassTransit;

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
    /// <param name="moduleConfigureConsumers">Collection of delegates to configure consumers within module assemblies.</param>
    /// <returns><see cref="IServiceCollection" /> with the infrastructure services registered.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        services.TryAddSingleton<DomainEventsInterceptor>();

        services.AddMassTransit(configure =>
        {
            foreach (Action<IRegistrationConfigurator> configureConsumers in moduleConfigureConsumers)
            {
                configureConsumers(configure);
            }

            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}