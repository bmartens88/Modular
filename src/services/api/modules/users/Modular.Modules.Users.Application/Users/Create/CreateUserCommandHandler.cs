using Modular.Common.Application.Messaging;
using Modular.Common.Domain.Abstractions;
using Modular.Common.Domain.Monads;
using Modular.Modules.Users.Domain;
using Modular.Modules.Users.Domain.Abstractions;

namespace Modular.Modules.Users.Application.Users.Create;

/// <summary>
///     Defines a command handler for the <see cref="CreateUserCommand" /> command.
/// </summary>
/// <param name="userRepository"><see cref="IUserRepository" /> implementation for interacting with persistence.</param>
/// <param name="unitOfWork"><see cref="IUnitOfWork" /> implementation to save all made changes.</param>
internal sealed class CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, User>
{
    /// <inheritdoc />
    public async Task<Result<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        (string firstName, string lastName, string email) = command;
        User user = User.Create(firstName, lastName, email);
        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return user;
    }
}