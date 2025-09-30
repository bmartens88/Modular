using Modular.Common.Domain.Abstractions;

namespace Modular.Common.Application.Messaging;

/// <summary>
///     A base implementation of <see cref="IDomainEventHandler{TDomainEvent}" />.
/// </summary>
/// <typeparam name="TDomainEvent">The type of domain event to handle.</typeparam>
public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    /// <inheritdoc />
    public abstract Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return HandleAsync((TDomainEvent)domainEvent, cancellationToken);
    }
}