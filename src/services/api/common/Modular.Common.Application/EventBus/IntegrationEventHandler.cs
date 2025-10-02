namespace Modular.Common.Application.EventBus;

/// <summary>
///     Defines a base implementation of <see cref="IIntegrationEventHandler{TIntegrationEvent}" />.
/// </summary>
/// <typeparam name="TIntegrationEvent">The type of the event to handle.</typeparam>
public abstract class IntegrationEventHandler<TIntegrationEvent> : IIntegrationEventHandler<TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
    /// <inheritdoc />
    public abstract Task HandleAsync(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public Task HandleAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        return HandleAsync((TIntegrationEvent)integrationEvent, cancellationToken);
    }
}