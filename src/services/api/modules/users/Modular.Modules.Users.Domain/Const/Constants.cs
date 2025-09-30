namespace Modular.Modules.Users.Domain.Const;

/// <summary>
///     Defines constants for the domain.
/// </summary>
public static class Constants
{
    /// <summary>
    ///     Constants specific to the Users domain.
    /// </summary>
    public static class Users
    {
        /// <summary>
        ///     The maximum length of the <see cref="User.FirstName" /> property.
        /// </summary>
        public const int FirstNameMaxLength = 100;

        /// <summary>
        ///     The maximum lenght of the <see cref="User.LastName" /> property.
        /// </summary>
        public const int LastNameMaxLength = 200;

        /// <summary>
        ///     The maximum length of the <see cref="User.Email" /> property.
        /// </summary>
        public const int EmailMaxLength = 300;
    }
}