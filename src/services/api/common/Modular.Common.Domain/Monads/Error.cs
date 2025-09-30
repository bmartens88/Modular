namespace Modular.Common.Domain.Monads;

/// <summary>
///     An error object.
/// </summary>
/// <param name="Code">The error code of this error.</param>
/// <param name="Description">The description of this error.</param>
/// <param name="Type">The type of this error, which is a value of <see cref="ErrorType" />.</param>
public record Error(string Code, string Description, ErrorType Type)
{
    /// <summary>
    ///     Returns an error which is known as no error.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    /// <summary>
    ///     Returns an error which conveys that a null value was provided when no null value should have been provided.
    /// </summary>
    public static readonly Error NullValue = new(
        "General.NullValue",
        "A null value was provided",
        ErrorType.Failure);

    /// <summary>
    ///     Returns a new error of type <see cref="ErrorType.Failure" />.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The description of this error.</param>
    /// <returns>An instance of <see cref="Error" /> of type <see cref="ErrorType.Failure" />.</returns>
    public static Error Failure(string code, string description)
    {
        return new Error(code, description, ErrorType.Failure);
    }

    /// <summary>
    ///     Returns a new error of type <see cref="ErrorType.Problem" />.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The description of this error.</param>
    /// <returns>An instance of <see cref="Error" /> of type <see cref="ErrorType.Problem" />.</returns>
    public static Error Problem(string code, string description)
    {
        return new Error(code, description, ErrorType.Problem);
    }

    /// <summary>
    ///     Returns a new error of type <see cref="ErrorType.NotFound" />.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The description of this error.</param>
    /// <returns>An instance of <see cref="Error" /> of type <see cref="ErrorType.NotFound" />.</returns>
    public static Error NotFound(string code, string description)
    {
        return new Error(code, description, ErrorType.NotFound);
    }

    /// <summary>
    ///     Returns a new error of type <see cref="ErrorType.Conflict" />.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The description of this error.</param>
    /// <returns>An instance of <see cref="Error" /> of type <see cref="ErrorType.Conflict" />.</returns>
    public static Error Conflict(string code, string description)
    {
        return new Error(code, description, ErrorType.Conflict);
    }
}