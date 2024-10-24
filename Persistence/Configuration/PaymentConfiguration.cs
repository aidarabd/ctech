using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payment", "main");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(x => x.Amount).HasColumnName("amount").IsRequired();
        builder.Property(x => x.OperationTypeId).HasColumnName("operation_type_id").IsRequired();
        builder.Property(x => x.OperationDate).HasColumnName("operation_date").IsRequired();
    }
}