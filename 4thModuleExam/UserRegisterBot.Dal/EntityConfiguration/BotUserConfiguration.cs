using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserRegisterBot.Dal.Entities;

namespace UserRegisterBot.Dal.EntityConfiguration;

public class BotUserConfiguration : IEntityTypeConfiguration<BotUser>
{
    public void Configure(EntityTypeBuilder<BotUser> builder)
    {
        builder.ToTable("BotUsers");

        builder.HasKey(u => u.BotUserId);

        builder.HasIndex(u => u.TelegramUserId).IsUnique(true);

        builder.Property(u => u.FirstName).
            IsRequired(true).
            HasMaxLength(100);

        builder.Property(u => u.LastName).
            IsRequired(true).
            HasMaxLength(100);

        builder.Property(u => u.Email).
            IsRequired(true).
            HasMaxLength(200);

        builder.Property(u => u.PhoneNumber).
            IsRequired(true).
            HasMaxLength(16);

        builder.Property(u => u.Address).
            IsRequired(true).
            HasMaxLength(50);

        builder.Property(u => u.DateOfBirth).
            IsRequired(true);
    }
}
