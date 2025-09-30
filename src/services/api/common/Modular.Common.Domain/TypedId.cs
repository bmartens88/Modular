using Ardalis.GuardClauses;

namespace Modular.Common.Domain;

/// <summary>
///     Provides a base implementation of a strongly typed identifier.
/// </summary>
/// <param name="value">The value to wrap with this typed identifier.</param>
/// <typeparam name="TValue">Type of the internal <see cref="Value" />.</typeparam>
public abstract class TypedId<TValue>(TValue value) : TypedId
    where TValue : struct
{
    /// <summary>
    ///     The value wrapped by this typed identifier.
    /// </summary>
    public TValue Value { get; } = Guard.Against.Default(value);

    /// <inheritdoc />
    protected sealed override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}

/// <summary>
///     Provides a base implementation of a strongly typed identifier.
/// </summary>
/// <remarks>This type is primarily used as an abstraction.</remarks>
public abstract class TypedId : ValueObject;