using Modular.Common.Domain.Events;

namespace Modular.Modules.Users.Domain.Events;

public sealed record UserCreatedDomainEvent(User User) : DomainEvent;