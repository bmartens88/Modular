using MassTransit;

using Modular.Common.Application.EventBus;

namespace Modular.Common.Infrastructure.EventBus;

/// <summary>
///     Implementation of <see cref="IEventBus" />.
/// </summary>
/// <param name="bus"><see cref="IBus" /> instance used for publishing events.</param>
internal sealed class EventBus(IBus bus) : IEventBus
{
    /// <inheritdoc />
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent
    {
        await bus.Publish(integrationEvent, cancellationToken);
    }
}