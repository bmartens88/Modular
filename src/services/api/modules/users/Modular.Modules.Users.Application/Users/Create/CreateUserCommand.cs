using Modular.Common.Application.Messaging;
using Modular.Modules.Users.Domain;

namespace Modular.Modules.Users.Application.Users.Create;

/// <summary>
///     Defines a command for creating a new <see cref="User" />.
/// </summary>
/// <param name="FirstName">The first name of the user.</param>
/// <param name="LastName">The last name of the user.</param>
/// <param name="Email">The email address of the user.</param>
public sealed record CreateUserCommand(string FirstName, string LastName, string Email) : ICommand<User>;