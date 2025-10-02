using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modular.Common.Infrastructure.Outbox;

/// <summary>
///     Defines configuration for the <see cref="OutboxMessage" /> entity type.
/// </summary>
internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");

        builder.HasKey(o => o.Id);

        // Use PostgreSQL's JSONB type to store the message content.'
        builder.Property(o => o.Content)
            .HasMaxLength(2000)
            .HasColumnType("jsonb");
    }
}