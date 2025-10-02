using System.Reflection;

using MassTransit.Internals;

using Microsoft.Extensions.DependencyInjection;

using Modular.Common.Application.Messaging;

namespace Modular.Common.Infrastructure.Outbox;

/// <summary>
///     Factory which can be used to get all domain event handlers for a given domain event.
/// </summary>
public static class DomainEventHandlersFactory
{
    // Serves as a cache for the handlers.
    private static readonly Dictionary<string, Type[]> HandlersDictionary = [];

    /// <summary>
    ///     Get all domain event handlers for the domain event with the given <paramref name="type" />.
    /// </summary>
    /// <param name="type">The type of the domaine vent.</param>
    /// <param name="serviceProvider"><see cref="IServiceProvider" /> instance for resolving services from the DI container.</param>
    /// <param name="assembly">The assembly in which to look for the domain event handlers.</param>
    /// <returns><see cref="IEnumerable{T}" /> of resolved domain event handlers.</returns>
    public static IEnumerable<IDomainEventHandler> GetHandlers(
        Type type,
        IServiceProvider serviceProvider,
        Assembly assembly)
    {
        Type[] domainEventHandlerTypes = HandlersDictionary.GetOrAdd(
            $"{assembly.GetName().Name}{type.Name}",
            _ =>
            {
                Type[] domainEventHandlerTypes = assembly.GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler<>).MakeGenericType(type)))
                    .ToArray();
                return domainEventHandlerTypes;
            });

        List<IDomainEventHandler> handlers = [];
        handlers.AddRange(domainEventHandlerTypes
            .Select(serviceProvider.GetRequiredService)
            .Select(domainEventHandler => (domainEventHandler as IDomainEventHandler)!));

        return handlers;
    }
}