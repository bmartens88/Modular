using MediatR;

using Microsoft.Extensions.Logging;

using Modular.Common.Application.Exceptions;

namespace Modular.Common.Application.Behaviors;

/// <summary>
///     <see cref="IPipelineBehavior{TRequest,TResponse}" /> implementation that handles exceptions.
/// </summary>
/// <param name="logger"><see cref="ILogger{TCategoryName}" /> abstraction to log caught error(s).</param>
/// <typeparam name="TRequest">Type of the request.</typeparam>
/// <typeparam name="TResponse">Type of the response.</typeparam>
internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception ex)
        {
            string requestName = typeof(TRequest).Name;
            logger.LogError(ex, "An unhandled exception occurred during processing of request {RequestName}",
                requestName);
            throw new ModularException(requestName, innerException: ex);
        }
    }
}