using Modular.Common.Domain.Monads;

namespace Modular.Common.Application.Exceptions;

/// <summary>
///     Custom exception implementation.
/// </summary>
/// <param name="requestName">The request in-processing during which the exception was thrown.</param>
/// <param name="error">Optional: The error which caused the exception to be thrown.</param>
/// <param name="innerException">Optional: The caught exception which caused this exception to be thrown.</param>
public sealed class ModularException(string requestName, Error? error = null, Exception? innerException = null)
    : Exception("Application exception", innerException)
{
    /// <summary>
    ///     The request in-processing during which an exception was thrown.
    /// </summary>
    /// <value>The name of the request which threw an exception.</value>
    public string RequestName { get; } = requestName;

    /// <summary>
    ///     Optional: The error which caused the exception to be thrown.
    /// </summary>
    /// <value>The error which caused the exception to be thrown.</value>
    public Error? Error { get; } = error;
}