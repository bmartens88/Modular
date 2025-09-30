using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;

namespace Modular.Common.Domain.Guards;

/// <summary>
///     Defines Guard extension methods.
/// </summary>
public static class LengthGuard
{
    /// <summary>
    ///     Defines a guard clause for string length.
    /// </summary>
    /// <param name="_"><see cref="IGuardClause" /> instance.</param>
    /// <param name="input">The input string to verify the length of.</param>
    /// <param name="maxLength">The maximum length of <paramref name="input" />.</param>
    /// <param name="paramName">Name of the expression passed as value for <paramref name="input" />.</param>
    /// <returns>
    ///     <paramref name="input" /> if the length does not exceed <paramref name="maxLength" />; otherwise an exception
    ///     is thrown.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///     Thrown when the length of <paramref name="input" /> exceeds that specified by
    ///     <paramref name="maxLength" />.
    /// </exception>
    public static string Length(this IGuardClause _,
        string input,
        int maxLength,
        [CallerArgumentExpression(nameof(input))]
        string? paramName = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);

        return input.Length >= maxLength
            ? throw new ArgumentException($"Should not exceed maximum length of {maxLength}", paramName)
            : input;
    }
}