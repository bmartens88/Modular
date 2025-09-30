using Modular.Modules.Users.Domain.ValueObjects;

namespace Modular.Modules.Users.Domain.Abstractions;

/// <summary>
///     Defines the contract for a user repository.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    ///     Get a <see cref="User" /> by id.
    /// </summary>
    /// <param name="id"><see cref="UserId" /> to search for.</param>
    /// <param name="cancellationToken">Propagates notification that processes should be canceled.</param>
    /// <returns>
    ///     A <see cref="Task{TResult}" /> which resembles the asynchronous operation. When it complets, it either returns
    ///     a <see cref="User" /> or <see langword="null" />.
    /// </returns>
    Task<User?> FindByIdAsync(UserId id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds the given <paramref name="user" /> to the repository.
    /// </summary>
    /// <param name="user">The <see cref="User" /> to add.</param>
    /// <remarks>The <see cref="User" /> will not be added to the database until <c>SaveChangesAsync</c> is called.</remarks>
    void Add(User user);
}