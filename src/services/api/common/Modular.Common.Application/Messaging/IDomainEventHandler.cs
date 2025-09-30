using Modular.Common.Domain.Abstractions;

namespace Modular.Common.Application.Messaging;

/// <summary>
///     Serves as an abstraction of a domain event handler.
/// </summary>
public interface IDomainEventHandler
{
    /// <summary>
    ///     Handles the given <paramref name="domainEvent" />.
    /// </summary>
    /// <param name="domainEvent">A domain event of type <see cref="IDomainEvent" /> to handle.</param>
    /// <param name="cancellationToken">Propagates notification that processes should be canceled.</param>
    /// <returns>A <see cref="Task" /> which resembles the asynchronous operation.</returns>
    Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}

/// <summary>
///     Serves as an abstraction of a domain event handler.
/// </summary>
/// <typeparam name="TDomainEvent">The type of domain event to handle.</typeparam>
public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler
    where TDomainEvent : IDomainEvent
{
    /// <summary>
    ///     Handles the given <paramref name="domainEvent" />.
    /// </summary>
    /// <param name="domainEvent">A domain event of type <typeparamref name="TDomainEvent" /> to handle.</param>
    /// <param name="cancellationToken">Propagates notification that processes should be canceled.</param>
    /// <returns>A <see cref="Task" /> wich resembles the asynchronous operation.</returns>
    Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}