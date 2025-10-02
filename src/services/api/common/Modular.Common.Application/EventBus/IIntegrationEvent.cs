namespace Modular.Common.Application.EventBus;

/// <summary>
///     Defines the contract of an integration event.
/// </summary>
public interface IIntegrationEvent
{
    /// <summary>
    ///     The unique id of this event, primarily used for idempotancy.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    ///     The moment in time (UTC) when this event occurred.
    /// </summary>
    DateTime OccurredOnUtc { get; }
}