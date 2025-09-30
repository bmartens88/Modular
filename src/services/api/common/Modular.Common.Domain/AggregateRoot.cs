using Modular.Common.Domain.Abstractions;

namespace Modular.Common.Domain;

/// <summary>
///     Provides a base implementation of an Aggregate (root).
/// </summary>
/// <typeparam name="TId">The strongly typed identifier type of the Aggregate.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : TypedId
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    ///     Instantiates a new instance of <see cref="AggregateRoot{TId}" />.
    /// </summary>
    /// <param name="id">The value of the identity to use.</param>
    protected AggregateRoot(TId id)
        : base(id)
    {
    }

    /// <summary>
    ///     Instantiates a new instance of <see cref="AggregateRoot{TId}" />.
    /// </summary>
    protected AggregateRoot()
    {
    }

    /// <inheritdoc />
    public IReadOnlyCollection<IDomainEvent> PopDomainEvents()
    {
        List<IDomainEvent> copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }

    /// <summary>
    ///     Adds the given <paramref name="domainEvent" /> to the internal list of events.
    /// </summary>
    /// <param name="domainEvent">The <see cref="IDomainEvent" /> instance to add.</param>
    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}