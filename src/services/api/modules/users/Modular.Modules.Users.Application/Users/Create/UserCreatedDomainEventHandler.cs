using Modular.Common.Application.Messaging;
using Modular.Modules.Users.Domain.Events;

namespace Modular.Modules.Users.Application.Users.Create;

// TODO: Add IEventBus reference and publish integration event
internal sealed class UserCreatedDomainEventHandler
    : DomainEventHandler<UserCreatedDomainEvent>
{
    public override Task HandleAsync(UserCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine("User was created!");
        return Task.CompletedTask;
    }
}