using System.Reflection;
using System.Runtime.CompilerServices;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Modular.Common.Domain.Monads;

namespace Modular.Common.Application.Behaviors;

/// <summary>
///     <see cref="IPipelineBehavior{TRequest,TResponse}" /> implementation that validates the given
///     <typeparamref name="TRequest" />.
/// </summary>
/// <param name="validators"><see cref="IEnumerable{T}" /> of validators.</param>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : class
{
    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next(cancellationToken);
        }

        ValidationFailure[] validationFailures = await ValidateAsync(validators.ToList(), request, cancellationToken);

        if (validationFailures.Length is 0)
        {
            return await next(cancellationToken);
        }

        if (typeof(TResponse) == typeof(Result))
        {
            return Unsafe.As<TResponse>(CreateValidationError(validationFailures));
        }

        if (!typeof(TResponse).IsGenericType ||
            typeof(TResponse).GetGenericTypeDefinition() != typeof(Result<>))
        {
            throw new ValidationException(validationFailures);
        }

        Type resultType = typeof(TResponse).GetGenericArguments()[0];
        MethodInfo? failureMethod = typeof(TResponse)
            .MakeGenericType(resultType)
            .GetMethod(nameof(Result<object>.ValidationResult));
        return failureMethod is not null
            ? Unsafe.As<TResponse>(failureMethod.Invoke(null, [CreateValidationError(validationFailures)]))!
            : throw new ValidationException(validationFailures);
    }

    /// <summary>
    ///     Validates the given <paramref name="request" /> agains the given <paramref name="validators" /> and returns
    ///     the validation errors (if any).
    /// </summary>
    /// <param name="validators"><see cref="IReadOnlyCollection{IValidator}" /> of validators.</param>
    /// <param name="request">The request of type <typeparamref name="TRequest" /> to validate.</param>
    /// <param name="cancellationToken">Propagates notification that processes should be canceled.</param>
    /// <returns>A <see cref="Task" /> which resembles the asynchronous operation.</returns>
    private static async Task<ValidationFailure[]> ValidateAsync(IReadOnlyCollection<IValidator<TRequest>> validators,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        if (validators.Count is 0)
        {
            return [];
        }

        ValidationContext<TRequest> context = new(request);

        ValidationResult[] validationResults =
            await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        return validationResults
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .ToArray();
    }

    /// <summary>
    ///     Creates a new instance of <see cref="ValidationError" /> with the given <paramref name="validationFailures" />.
    /// </summary>
    /// <param name="validationFailures">Any failures occurred during validation.</param>
    /// <returns>A new instance of <see cref="ValidationError" />.</returns>
    private static ValidationError CreateValidationError(ValidationFailure[] validationFailures)
    {
        return new ValidationError(validationFailures
            .Select(failure => Error.Problem(failure.ErrorCode, failure.ErrorMessage)).ToArray());
    }
}