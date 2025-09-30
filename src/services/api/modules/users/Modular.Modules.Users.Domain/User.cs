using Ardalis.GuardClauses;

using Modular.Common.Domain;
using Modular.Common.Domain.Guards;
using Modular.Modules.Users.Domain.Events;
using Modular.Modules.Users.Domain.ValueObjects;

namespace Modular.Modules.Users.Domain;

public sealed class User : AggregateRoot<UserId>
{
    private User(
        UserId id,
        string firstName,
        string lastName,
        string email)
        : base(id)
    {
        FirstName = Guard.Against.Length(firstName, 100);
        LastName = Guard.Against.Length(lastName, 200);
        Email = Guard.Against.Length(email, 300);
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        UserId? id = null)
    {
        User user = new(id ?? UserId.New(), firstName, lastName, email);
        user.Raise(new UserCreatedDomainEvent(user));
        return user;
    }
}