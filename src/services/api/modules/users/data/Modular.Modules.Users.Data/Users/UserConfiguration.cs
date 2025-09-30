using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Modular.Modules.Users.Domain;
using Modular.Modules.Users.Domain.Const;
using Modular.Modules.Users.Domain.ValueObjects;

namespace Modular.Modules.Users.Data.Users;

/// <summary>
///     Defines configuration for the <see cref="User" /> entity type.
/// </summary>
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(id => id.Value,
                value => UserId.Create(value));

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(Constants.Users.FirstNameMaxLength);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(Constants.Users.LastNameMaxLength);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(Constants.Users.EmailMaxLength);

        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}