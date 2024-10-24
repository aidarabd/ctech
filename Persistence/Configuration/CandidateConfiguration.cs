using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("candidates", "main");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(x => x.LastName).HasColumnName("last_name").IsRequired();
        builder.Property(x => x.FirstName).HasColumnName("first_name").IsRequired();
        builder.Property(x => x.Email).HasColumnName("email").IsRequired();
        builder.Property(x => x.Comment).HasColumnName("comment").IsRequired();
        builder.Property(x => x.PhoneNumber).HasColumnName("phone_number");
        builder.Property(x => x.AvailableFrom).HasColumnName("available_from");
        builder.Property(x => x.AvailableTo).HasColumnName("available_to");
        builder.Property(x => x.LinkedinUrl).HasColumnName("linkedin_url");
        builder.Property(x => x.GithubUrl).HasColumnName("github_url");

        builder.HasIndex(x => x.Email).IsUnique();
    }
}