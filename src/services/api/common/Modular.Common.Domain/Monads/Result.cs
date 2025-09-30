using System.Diagnostics.CodeAnalysis;

namespace Modular.Common.Domain.Monads;

/// <summary>
///     A result.
/// </summary>
public class Result
{
    /// <summary>
    ///     Instantiates a new instance of <see cref="Result" />.
    /// </summary>
    /// <param name="isSuccess"><see langword="true" /> if the result resembles a success; otherwise <see langword="false" />.</param>
    /// <param name="error">
    ///     <see cref="Error" /> instance containing either an error if <paramref name="isSuccess" /> is
    ///     <see langword="false" />, or <see cref="Monads.Error.None" /> if <paramref name="isSuccess" /> is
    ///     <see langword="true" />.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     Thrown when the given criteria for both <paramref name="isSuccess" /> and
    ///     <paramref name="error" /> are not met.
    /// </exception>
    protected Result(bool isSuccess, Error error)
    {
        if ((!isSuccess && error == Error.None) ||
            (isSuccess && error != Error.None))
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    ///     <see langword="true" /> if this result resembles a success; otherwise <see langword="false" />.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    ///     The direct opposite of <see cref="IsSuccess" />.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    ///     The error of this result, <see cref="Monads.Error.None" /> if <see cref="IsSuccess" /> is
    ///     <see langword="true" />, or an actual error when <see cref="IsSuccess" /> is <see langword="false" />.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    ///     Returns a successful result instance.
    /// </summary>
    /// <returns><see cref="Result" /> instance with <see cref="IsSuccess" /> set to <see langword="true" />.</returns>
    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    /// <summary>
    ///     Returns a failed result with the given <paramref name="error" />.
    /// </summary>
    /// <param name="error"><see cref="Monads.Error" /> instance resembling the error that occurred.</param>
    /// <returns><see cref="Result" /> instance with <see cref="IsSuccess" /> set to <see langword="false" />.</returns>
    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    /// <summary>
    ///     Returns a successful result with the given <paramref name="value" />.
    /// </summary>
    /// <param name="value">A value which is returned by this result.</param>
    /// <typeparam name="TValue">The type of the given <paramref name="value" />.</typeparam>
    /// <returns>
    ///     <see cref="Result{TValue}" /> instance with the given <paramref name="value" /> and <see cref="IsSuccess" />
    ///     set to <see langword="true" />.
    /// </returns>
    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, Error.None);
    }

    /// <summary>
    ///     Returns a failed result with the given <paramref name="error" />.
    /// </summary>
    /// <param name="error"><see cref="Monads.Error" /> instance resembling the error that occurred.</param>
    /// <typeparam name="TValue">The value type of this result, had it been a success.</typeparam>
    /// <returns><see cref="Result{TValue}" /> instance with the given <paramref name="error" />.</returns>
    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(default, false, error);
    }
}

/// <summary>
///     Generic implementation of <see cref="Result" /> with a value.
/// </summary>
/// <param name="value">Optional: The value of this result.</param>
/// <param name="isSuccess"><see langword="true" /> if this result resembles a success; otherwise <see langword="false" />.</param>
/// <param name="error">
///     <see cref="Error" /> instance containing either an error if <paramref name="isSuccess" /> is
///     <see langword="false" />, or <see cref="Monads.Error.None" /> if <paramref name="isSuccess" /> is
///     <see langword="true" />.
/// </param>
/// <typeparam name="TValue">Type of the given <paramref name="value" />.</typeparam>
public sealed class Result<TValue>(TValue? value, bool isSuccess, Error error) : Result(isSuccess, error)
{
    private readonly TValue? _value = value;

    /// <summary>
    ///     The value of this result.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the value of a failed result is being accessed.</exception>
    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failed result can't be accessed.");

    /// <summary>
    ///     Implicitly converts the given <paramref name="value" /> to an instance of <see cref="Result{TValue}" />.
    /// </summary>
    /// <param name="value">The value of this result.</param>
    /// <returns>An instance of <see cref="Result{TValue}" /> with the given <paramref name="value" />.</returns>
    public static implicit operator Result<TValue>(TValue? value)
    {
        return value is null ? Failure<TValue>(Error.NullValue) : Success(value);
    }

    /// <summary>
    ///     Instantiates a new instance of <see cref="Result{TValue}" /> with the given <paramref name="error" />.
    /// </summary>
    /// <param name="error"><see cref="Monads.Error" /> instance resembling the error that occurred.</param>
    /// <returns><see cref="Result{TValue}" /> instance with the given <paramref name="error" />.</returns>
    /// <remarks>This method is typically used by the validation pipeline behavior.</remarks>
    public static Result<TValue> ValidationResult(Error error)
    {
        return new Result<TValue>(default, false, error);
    }
}