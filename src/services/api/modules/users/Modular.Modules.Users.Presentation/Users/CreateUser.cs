namespace Modular.Modules.Users.Presentation.Users;

/// <summary>
///     Defines types for both command/query and response for the <see cref="CreateUserEndpoint" /> class.
/// </summary>
internal static class CreateUser
{
    /// <summary>
    ///     Defines the command which is sent by the client.
    /// </summary>
    /// <param name="FirstName">The first name of the user.</param>
    /// <param name="LastName">The last name of the user.</param>
    /// <param name="Email">The email address of the user.</param>
    internal record Command(string FirstName, string LastName, string Email);

    /// <summary>
    ///     Defines the response which is sent by the server.
    /// </summary>
    /// <param name="Id">The id of the user.</param>
    /// <param name="FirstName">The first name of the user.</param>
    /// <param name="LastName">The last name of the user.</param>
    /// <param name="Email">The email address of the user.</param>
    internal record Response(Guid Id, string FirstName, string LastName, string Email);
}