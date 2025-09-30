using MediatR;

namespace Modular.Common.Domain.Abstractions;

/// <summary>
///     Defines the contract for a domain event.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    ///     The timestamp (UTC) the event occurred.
    /// </summary>
    DateTime OccurredOnUtc { get; }
}