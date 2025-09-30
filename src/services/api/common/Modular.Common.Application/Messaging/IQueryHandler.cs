using MediatR;
using Modular.Common.Domain.Monads;

namespace Modular.Common.Application.Messaging;

/// <summary>
///     Serves as an abstraction of a query handler, which returns a certain response.
/// </summary>
/// <typeparam name="TQuery">The query type of this query handler.</typeparam>
/// <typeparam name="TResponse">The response type of this query handler.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;