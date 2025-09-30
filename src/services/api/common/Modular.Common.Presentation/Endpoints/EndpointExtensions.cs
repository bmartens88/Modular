using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Modular.Common.Presentation.Endpoints;

/// <summary>
///     Defines extension methods for endpoints.
/// </summary>
public static class EndpointExtensions
{
    /// <summary>
    ///     Add all implementations of <see cref="IEndpoint" /> as a transient service descriptor to the DI container.
    /// </summary>
    /// <param name="services">The DI container for registering endpoints.</param>
    /// <param name="moduleAssemblies">The assemblies to scan for endpoints.</param>
    /// <returns><see cref="IServiceCollection" /> with the endpoints registered.</returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection services,
        params Assembly[] moduleAssemblies)
    {
        ServiceDescriptor[] serviceDescriptors = moduleAssemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    /// <summary>
    ///     Maps all the <see cref="IEndpoint" /> implementations registered in the DI container.
    /// </summary>
    /// <param name="app"><see cref="WebApplication" /> to use for mapping routes and endpoints.</param>
    /// <param name="routeGroupBuilder"><see cref="RouteGroupBuilder" /> to use for mapping routes and endpoints.</param>
    /// <returns><see cref="WebApplication" /> with all endpoints mapped.</returns>
    public static WebApplication MapEndpoints(this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = (IEndpointRouteBuilder?)routeGroupBuilder ?? app;

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
}