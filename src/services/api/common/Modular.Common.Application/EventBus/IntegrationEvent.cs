namespace Modular.Common.Application.EventBus;

/// <summary>
///     Defines a base implementation of <see cref="IIntegrationEvent" />.
/// </summary>
/// <param name="Id">The unique identifier of this event.</param>
/// <param name="OccurredOnUtc">The moment in time (UTC) this event occurred.</param>
public abstract record IntegrationEvent(Guid Id, DateTime OccurredOnUtc) : IIntegrationEvent;