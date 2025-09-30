namespace Modular.Common.Domain.Monads;

/// <summary>
///     An implementation of <see cref="Error" /> specifically for validation.
/// </summary>
/// <param name="Errors">Errors that occurred during validation.</param>
public sealed record ValidationError(Error[] Errors)
    : Error("General.Validation",
        "One or more validation errors occurred.",
        ErrorType.Validation)
{
    /// <summary>
    ///     Instantiates a new instance of <see cref="ValidationError" /> based on the given <paramref name="results" />.
    /// </summary>
    /// <param name="results"><see cref="IEnumerable{Result}" /> containing results.</param>
    /// <returns>An instance of <see cref="ValidationError" /> with the given result error(s).</returns>
    public static ValidationError FromResults(IEnumerable<Result> results)
    {
        return new ValidationError(results.Where(result => result.IsFailure).Select(result => result.Error).ToArray());
    }
}