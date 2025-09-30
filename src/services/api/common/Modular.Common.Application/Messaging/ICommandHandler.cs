using MediatR;
using Modular.Common.Domain.Monads;

namespace Modular.Common.Application.Messaging;

/// <summary>
///     Serves as an abstraction of a command handler.
/// </summary>
/// <typeparam name="TCommand">The command type of this command handler.</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

/// <summary>
///     Serves as an abstraction of a command handler, which returns a certain response.
/// </summary>
/// <typeparam name="TCommand">The command type of this command handler.</typeparam>
/// <typeparam name="TResponse">The response type of this command handler.</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;