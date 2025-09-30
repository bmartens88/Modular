namespace Modular.Modules.Users.Presentation.Users.GetById;

/// <summary>
///     Defines types for both command/query and response for the <see cref="GetUserByIdEndpoint" /> class.
/// </summary>
internal static class GetUserById
{
    /// <summary>
    ///     Defines the response which is sent by the server.
    /// </summary>
    /// <param name="Id">The id of the user.</param>
    /// <param name="FirstName">The first name of the user.</param>
    /// <param name="LastName">The last name of the user.</param>
    /// <param name="Email">The email address of the user.</param>
    internal sealed record Response(Guid Id, string FirstName, string LastName, string Email);
}