namespace Modular.Common.Domain;

/// <summary>
///     Provides a base implementation of an Entity.
/// </summary>
/// <typeparam name="TId">The strongly typed identifier type of the Entity.</typeparam>
/// <remarks>An entity is an object of which equality is based on the identifier.</remarks>
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : TypedId
{
    /// <summary>
    ///     Instantiates a new instance of <see cref="Entity{TId}" />.
    /// </summary>
    /// <param name="id">The value of the identity to use.</param>
    protected Entity(TId id)
    {
        Id = id;
    }

#pragma warning disable CS8618
    /// <summary>
    ///     Instantiates a new instance of <see cref="Entity{TId}" />.
    /// </summary>
    protected Entity()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    ///     The identifier of the <see cref="Entity{TId}" />.
    /// </summary>
    public TId Id { get; }

    /// <inheritdoc />
    public bool Equals(Entity<TId>? other)
    {
        return other is not null &&
               Id.Equals(other.Id);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as Entity<TId>);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    ///     Overload of the 'equals' operator.
    /// </summary>
    /// <param name="left">The first operand of type <see cref="Entity{TId}" />.</param>
    /// <param name="right">The second operand of type <see cref="Entity{TId}" />.</param>
    /// <returns><see langword="true" /> if both operands are considered to be equal; otherwise <see langword="false" />.</returns>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    ///     Overload of the 'not equals' operator.
    /// </summary>
    /// <param name="left">The first operand of type <see cref="Entity{TId}" />.</param>
    /// <param name="right">The second operand of type <see cref="Entity{TId}" />.</param>
    /// <returns><see langword="true" /> if both operand are considered to be not equal; otherwise <see langword="false" />.</returns>
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }
}