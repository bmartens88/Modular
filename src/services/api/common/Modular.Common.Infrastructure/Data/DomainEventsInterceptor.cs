using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using Modular.Common.Domain.Abstractions;
using Modular.Common.Infrastructure.Outbox;

namespace Modular.Common.Infrastructure.Data;

/// <summary>
///     Implementation of <see cref="SaveChangesInterceptor" /> which published registered domain events (if any).
/// </summary>
public sealed class DomainEventsInterceptor : SaveChangesInterceptor
{
    /// <inheritdoc />
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is { } dbContext)
        {
            InsertOutboxMessages(dbContext);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    ///     Converts all registered domain events (if any) to <see cref="OutboxMessage" /> and inserts them into the database.
    /// </summary>
    /// <param name="dbContext">The context used for database interaction.</param>
    private static void InsertOutboxMessages(DbContext dbContext)
    {
        OutboxMessage[] outboxMessages = dbContext.ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => entity.PopDomainEvents())
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = domainEvent.GetType().Name,
                Content = JsonSerializer.Serialize(domainEvent, JsonSerializerOptions.Default),
                OccurredOnUtc = DateTime.UtcNow
            })
            .ToArray();

        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}