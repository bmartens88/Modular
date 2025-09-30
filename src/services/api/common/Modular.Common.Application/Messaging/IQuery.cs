using MediatR;
using Modular.Common.Domain.Monads;

namespace Modular.Common.Application.Messaging;

/// <summary>
///     Serves as an abstraction of a query, which returns a certain response.
/// </summary>
/// <typeparam name="TResponse">The response type of this query.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;