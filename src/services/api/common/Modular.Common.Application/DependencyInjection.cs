using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using Modular.Common.Application.Behaviors;

namespace Modular.Common.Application;

/// <summary>
///     Defines extension methods for dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Adds all application services to the DI container.
    /// </summary>
    /// <param name="services">The DI container for registering application services.</param>
    /// <param name="moduleAssemblies">The assemblies to scan for handlers and/or validators.</param>
    /// <returns><see cref="IServiceCollection" /> with the application services registered.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services,
        params Assembly[] moduleAssemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);

            config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;
    }
}