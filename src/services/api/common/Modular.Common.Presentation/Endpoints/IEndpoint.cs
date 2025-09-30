using Microsoft.AspNetCore.Routing;

namespace Modular.Common.Presentation.Endpoints;

/// <summary>
///     Defines the contract for an endpoint.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    ///     Maps the endpoint to the given <paramref name="app" />.
    /// </summary>
    /// <param name="app"><see cref="IEndpointRouteBuilder" /> implementation for registering routes and endpoints.</param>
    void MapEndpoint(IEndpointRouteBuilder app);
}