using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Modular.Common.Domain.Abstractions;

namespace Modular.Common.Infrastructure.Data;

/// <summary>
///     Implementation of <see cref="SaveChangesInterceptor" /> which published registered domain events (if any).
/// </summary>
/// <param name="publisher"><see cref="IPublisher" /> implementation used for publishing the events.</param>
public sealed class DomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is { } dbContext)
        {
            await PublishDomainEventsAsync(dbContext, cancellationToken);
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    ///     Publishes all registered domain events on any entry of type <see cref="IAggregateRoot" />.
    /// </summary>
    /// <param name="context"><see cref="DbContext" /> abstraction used for database interaction.</param>
    /// <param name="cancellationToken">Propagates notification that processes should be canceled.</param>
    private async Task PublishDomainEventsAsync(DbContext context, CancellationToken cancellationToken = default)
    {
        IDomainEvent[] domainEvents = context
            .ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => entity.PopDomainEvents())
            .ToArray();

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }
    }
}