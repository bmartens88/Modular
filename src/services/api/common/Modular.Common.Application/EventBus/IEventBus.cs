namespace Modular.Common.Application.EventBus;

/// <summary>
///     Defines the contract for an event bus.
/// </summary>
public interface IEventBus
{
    /// <summary>
    ///     Publish an integration event on the bus.
    /// </summary>
    /// <param name="integrationEvent"><see cref="IIntegrationEvent" /> instance.</param>
    /// <param name="cancellationToken">Propagates notification that processes should be canceled.</param>
    /// <typeparam name="T">The type of the <paramref name="integrationEvent" />.</typeparam>
    /// <returns>A <see cref="Task" /> which resembles the asynchronous operation.</returns>
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent;
}