namespace Modular.Common.Infrastructure.Outbox;

/// <summary>
///     Defines the model of an Outbox message.
/// </summary>
public sealed class OutboxMessage
{
    /// <summary>
    ///     The unique identifier of the outbox message.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    ///     String representation of the type the <see cref="Content" /> should be deserialized to.
    /// </summary>
    public required string Type { get; init; }

    /// <summary>
    ///     Serialized content of an event.
    /// </summary>
    public required string Content { get; init; }

    /// <summary>
    ///     The moment in time (UTC) this event occurred.
    /// </summary>
    public required DateTime OccurredOnUtc { get; init; }

    /// <summary>
    ///     The moment in time (UTC) this event was processed.
    /// </summary>
    public DateTime? ProcessedOnUtc { get; set; }

    /// <summary>
    ///     When an exception occurs during processing, the error message is stored here.
    /// </summary>
    public string? Error { get; set; }
}