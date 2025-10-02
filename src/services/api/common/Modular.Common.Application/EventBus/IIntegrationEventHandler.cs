namespace Modular.Common.Application.EventBus;

/// <summary>
///     Defines the contract of an integration event handler.
/// </summary>
/// <typeparam name="TIntegrationEvent">The type of the integration event to handle.</typeparam>
public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IIntegrationEvent
{
    /// <summary>
    ///     Handles the given <paramref name="integrationEvent" />.
    /// </summary>
    /// <param name="integrationEvent"><typeparamref name="TIntegrationEvent" /> to handle.</param>
    /// <param name="cancellationToken">Propagates notification that processes should be canceled.</param>
    /// <returns>A <see cref="Task" /> which resembles the asynchronous operation.</returns>
    Task HandleAsync(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
}

/// <summary>
///     Defines the contract of an integration event handler.
/// </summary>
public interface IIntegrationEventHandler
{
    /// <summary>
    ///     Handles the given <paramref name="integrationEvent" />.
    /// </summary>
    /// <param name="integrationEvent"><see cref="IIntegrationEvent" /> instance to handle.</param>
    /// <param name="cancellationToken">Propagates notification that processes should be canceled.</param>
    /// <returns>A <see cref="Task" /> which resembles the asynchronous operation.</returns>
    Task HandleAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
}