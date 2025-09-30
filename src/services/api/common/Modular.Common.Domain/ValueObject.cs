namespace Modular.Common.Domain;

/// <summary>
///     Provides a base implementation of a value object.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <inheritdoc />
    public bool Equals(ValueObject? other)
    {
        return other is not null &&
               GetEqualityComponents()
                   .SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    ///     Gets a collection of equality components.
    /// </summary>
    /// <returns><see cref="IEnumerable{T}" /> of equality components.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as ValueObject);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    ///     Overload of the 'equals' operator.
    /// </summary>
    /// <param name="left">The first operand of type <see cref="ValueObject" />.</param>
    /// <param name="right">The second operand of type <see cref="ValueObject" />.</param>
    /// <returns><see langword="true" /> if both operands are considered equal; otherwise <see langword="false" />.</returns>
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    ///     Overload of the 'not equals' operator.
    /// </summary>
    /// <param name="left">The first operand of type <see cref="ValueObject" />.</param>
    /// <param name="right">The second operand of type <see cref="ValueObject" />.</param>
    /// <returns><see langword="true" /> if both operands are considered not equal; otherwise <see langword="false" />.</returns>
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }
}