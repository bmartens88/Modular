using Modular.Common.Domain;

namespace Modular.Modules.Users.Domain.ValueObjects;

/// <summary>
///     Strongly typed identifier for the <see cref="User" /> aggregate.
/// </summary>
public sealed class UserId : TypedId<Guid>
{
    /// <summary>
    ///     Instantiates a new instance of <see cref="UserId" />.
    /// </summary>
    /// <param name="value">The value to wrap, of type <see cref="Guid" />.</param>
    /// <remarks>
    ///     The constructor is marked private as to now allow instantiating without the use of one of the factory methods
    ///     defined within the <see cref="UserId" /> class.
    /// </remarks>
    private UserId(Guid value) : base(value)
    {
    }

    /// <summary>
    ///     Creates a new instance of <see cref="UserId" /> with the given <paramref name="value" />.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <returns>A new instance of <see cref="UserId" />.</returns>
    public static UserId Create(Guid value)
    {
        return new UserId(value);
    }

    /// <summary>
    ///     Creates a new instance of <see cref="UserId" /> with a unique value.
    /// </summary>
    /// <returns>A new instance of <see cref="UserId" />.</returns>
    public static UserId New()
    {
        return new UserId(Guid.NewGuid());
    }
}