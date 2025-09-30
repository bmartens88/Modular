using Modular.Common.Domain.Abstractions;

namespace Modular.Common.Domain.Events;

/// <summary>
///     Defines a base class for a <see cref="IDomainEvent" /> implementation.
/// </summary>
public abstract record DomainEvent(DateTime OccurredOnUtc) : IDomainEvent
{
    /// <summary>
    ///     Instantiates a new instance of <see cref="DomainEvent" />.
    /// </summary>
    /// <remarks>This will set the value of the <see cref="OccurredOnUtc" /> property to <see cref="DateTime.UtcNow" />.</remarks>
    protected DomainEvent()
        : this(DateTime.UtcNow)
    {
    }
}