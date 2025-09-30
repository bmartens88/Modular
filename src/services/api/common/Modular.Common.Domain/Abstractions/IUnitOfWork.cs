namespace Modular.Common.Domain.Abstractions;

/// <summary>
///     Abstraction used for the Unit of Work pattern.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Saves all made changes in an atomic fashion.
    /// </summary>
    /// <param name="cancellationToken">Propagates notification that processes should be canceled.</param>
    /// <returns>
    ///     A <see cref="Task{TResult}" /> which resembles the asynchronous operation. When it completes, return the
    ///     number of affected rows.
    /// </returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}