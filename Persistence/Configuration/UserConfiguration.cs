using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", "main", t => t
            .HasCheckConstraint("CK_NegativeValue_Negative", "\"balance\" > 0"));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(x => x.Username).HasColumnName("username").IsRequired();
        builder.Property(x => x.PasswordHash).HasColumnName("password_hash").IsRequired();
        builder.Property(x => x.Balance).HasColumnName("balance").IsRequired();
        builder.Property(x => x.LoginAttempts).HasColumnName("login_attempt");
        builder.Property(x => x.LockoutEnd).HasColumnName("lock_end");
        
        builder.HasIndex(x => x.Username).IsUnique();
    }
}