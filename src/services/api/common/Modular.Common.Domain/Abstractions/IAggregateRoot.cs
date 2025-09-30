namespace Modular.Common.Domain.Abstractions;

/// <summary>
///     Defines the contract for an aggregate (root).
/// </summary>
public interface IAggregateRoot
{
    /// <summary>
    ///     Returns a copy of all registered <see cref="IDomainEvent" /> of this aggregate.
    /// </summary>
    /// <returns><see cref="IReadOnlyCollection{IDomainEvent}" /> containing the registered events of the current aggregate.</returns>
    IReadOnlyCollection<IDomainEvent> PopDomainEvents();
}