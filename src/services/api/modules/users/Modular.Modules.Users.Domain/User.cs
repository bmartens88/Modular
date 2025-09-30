using Ardalis.GuardClauses;

using Modular.Common.Domain;
using Modular.Common.Domain.Guards;
using Modular.Modules.Users.Domain.Const;
using Modular.Modules.Users.Domain.Events;
using Modular.Modules.Users.Domain.ValueObjects;

namespace Modular.Modules.Users.Domain;

/// <summary>
///     User Aggregate (Root).
/// </summary>
public sealed class User : AggregateRoot<UserId>
{
    /// <summary>
    ///     Instantiates a new instance of <see cref="User" />.
    /// </summary>
    /// <param name="id"><see cref="UserId" /> strongly typed identifier.</param>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="email">The email address of the user.</param>
    private User(
        UserId id,
        string firstName,
        string lastName,
        string email)
        : base(id)
    {
        FirstName = Guard.Against.Length(firstName, Constants.Users.FirstNameMaxLength);
        LastName = Guard.Against.Length(lastName, Constants.Users.LastNameMaxLength);
        Email = Guard.Against.Length(email, Constants.Users.EmailMaxLength);
    }

    /// <summary>
    ///     The first name of the user.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    ///     The last name of the user.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    ///     The email address of the user.
    /// </summary>
    public string Email { get; }

    /// <summary>
    ///     Creates a new instance of <see cref="User" />.
    /// </summary>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="id">Optional: <see cref="UserId" /> strongly typed identifier.</param>
    /// <returns>A new instance of <see cref="User" />.</returns>
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