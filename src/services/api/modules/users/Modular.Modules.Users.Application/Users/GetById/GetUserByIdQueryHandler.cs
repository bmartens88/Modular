using Modular.Common.Application.Messaging;
using Modular.Common.Domain.Monads;
using Modular.Modules.Users.Domain;
using Modular.Modules.Users.Domain.Abstractions;
using Modular.Modules.Users.Domain.Errors;

namespace Modular.Modules.Users.Application.Users.GetById;

/// <summary>
///     Defines a query handler for the <see cref="GetUserByIdQuery" /> query.
/// </summary>
/// <param name="userRepository"><see cref="IUserRepository" /> implementation for interacting with persistence.</param>
internal sealed class GetUserByIdQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUserByIdQuery, User>
{
    /// <inheritdoc />
    public async Task<Result<User>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FindByIdAsync(query.UserId, cancellationToken);
        return user ?? Result.Failure<User>(Errors.Users.NotFound(query.UserId));
    }
}