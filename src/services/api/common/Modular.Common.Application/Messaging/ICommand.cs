using MediatR;
using Modular.Common.Domain.Monads;

namespace Modular.Common.Application.Messaging;

/// <summary>
///     Serves as an abstraction of a command.
/// </summary>
public interface ICommand : IRequest<Result>, IBaseCommand;

/// <summary>
///     Serves as an abstraction of a command, which returns a certain response.
/// </summary>
/// <typeparam name="TResponse">The response type of this command.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

/// <summary>
///     An overall abstraction of a command in the system.
/// </summary>
public interface IBaseCommand;