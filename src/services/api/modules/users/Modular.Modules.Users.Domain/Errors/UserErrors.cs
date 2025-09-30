using Modular.Common.Domain.Monads;
using Modular.Modules.Users.Domain.ValueObjects;

namespace Modular.Modules.Users.Domain.Errors;

public static class Errors
{
    /// <summary>
    ///     Defines different errors which can be returned when dealing with users.
    /// </summary>
    public static class Users
    {
        /// <summary>
        ///     Returns an <see cref="Error" /> with type <see cref="ErrorType.NotFound" />.
        /// </summary>
        /// <param name="userId">The <see cref="UserId" /> which wasn't found.</param>
        /// <returns>
        ///     <see cref="Error" /> with type <see cref="ErrorType.NotFound" /> and a message that the given
        ///     <paramref name="userId" /> yielded no results.
        /// </returns>
        public static Error NotFound(UserId userId)
        {
            return Error.NotFound("Errors.Users.NotFound",
                $"A user with identifier '{userId.Value}' was not found.");
        }
    }
}