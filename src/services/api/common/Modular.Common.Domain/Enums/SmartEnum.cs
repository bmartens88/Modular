using System.Diagnostics.CodeAnalysis;

namespace Modular.Common.Domain.Enums;

/// <summary>
///     Defines a base class for a smart enum.
/// </summary>
/// <typeparam name="TEnum">Type of the smart enum.</typeparam>
/// <remarks>This class defaults the type of the value to <see langword="int" />.</remarks>
public abstract record SmartEnum<TEnum> : SmartEnum<TEnum, int>
    where TEnum : SmartEnum<TEnum, int>
{
    /// <summary>
    ///     Instantiates a new instance of <see cref="SmartEnum{TEnum, int}" />.
    /// </summary>
    /// <param name="name">The name of the smart enum.</param>
    /// <param name="value">The value of the smart enum.</param>
    protected SmartEnum(string name, int value)
        : base(name, value)
    {
    }
}

/// <summary>
///     Defines a base class for a smart enum.
/// </summary>
/// <typeparam name="TEnum">Type of the smart enum.</typeparam>
/// <typeparam name="TValue">Type of the <see cref="Value" /> of the smart enum.</typeparam>
public abstract record SmartEnum<TEnum, TValue>
    where TEnum : SmartEnum<TEnum, TValue>
    where TValue : IEquatable<TValue>
{
    private static readonly List<TEnum> EnumValues = [];

    /// <summary>
    ///     Instantiates a new instance of <see cref="SmartEnum{TEnum, TValue}" />.
    /// </summary>
    /// <param name="name">The name of the smart enum.</param>
    /// <param name="value">The value of the smart enum.</param>
    protected SmartEnum(string name, TValue value)
    {
        Name = name;
        Value = value;
        EnumValues.Add((TEnum)this);
    }

    /// <summary>
    /// The name of the smart enum.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The value of the smart enum.
    /// </summary>
    public TValue Value { get; }

    /// <summary>
    ///     Tries to get a smart enum from a given name.
    /// </summary>
    /// <param name="name">The name of the smart enum to find.</param>
    /// <param name="result">When this method returns, either contains the found smart enum or <see langword="null" />.</param>
    /// <returns><see langword="true" /> when a value was found; otherwise <see langword="false" />.</returns>
    public bool TryFromName(string name, [NotNullWhen(true)] out TEnum? result)
    {
        result = EnumValues.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return result is not null;
    }

    /// <summary>
    ///     Tries to get a smart enum from a given value.
    /// </summary>
    /// <param name="value">The value of the smart enum to find.</param>
    /// <param name="result">When this method returns, either contains the found smart enum or <see langword="null" />.</param>
    /// <returns><see langword="true" /> when a value was found; otherwise <see langword="false" />.</returns>
    public bool TryFromValue(TValue value, [NotNullWhen(true)] out TEnum? result)
    {
        result = EnumValues.FirstOrDefault(e => e.Value.Equals(value));
        return result is not null;
    }

    /// <summary>
    ///     Get a smart enum from a given name.
    /// </summary>
    /// <param name="name">The name of the smart enum to find.</param>
    /// <returns>The smart enum with the given <paramref name="name" />.</returns>
    /// <exception cref="ArgumentException">Thrown when no smart enum with the given <paramref name="name" /> was found.</exception>
    public TEnum FromName(string name)
    {
        return TryFromName(name, out var result)
            ? result
            : throw new ArgumentException("Invalid value", nameof(name));
    }

    /// <summary>
    ///     Get a smart enum from a given value.
    /// </summary>
    /// <param name="value">The value of the smart enum to find.</param>
    /// <returns>The smart enum with the given <paramref name="value" />.</returns>
    /// <exception cref="ArgumentException">Thrown when no smart enum with the given <paramref name="value" /> was found.</exception>
    public TEnum FromValue(TValue value)
    {
        return TryFromValue(value, out var result)
            ? result
            : throw new ArgumentException("Invalid value", nameof(value));
    }
}