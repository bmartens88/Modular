using Modular.Common.Domain.Events;

namespace Modular.Modules.Users.Domain.Events;

/// <summary>
///     Domain event for when a new <see cref="User" /> is created.
/// </summary>
/// <param name="User">The <see cref="User" /> which was created.</param>
/// <remarks>The given <paramref name="User" /> will also contain this domain event.</remarks>
public sealed record UserCreatedDomainEvent(User User) : DomainEvent;