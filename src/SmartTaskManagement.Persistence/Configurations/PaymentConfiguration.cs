using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartTaskManagement.Domain.Entities;

namespace SmartTaskManagement.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        // Primary Key
        builder.HasKey(x => x.Id);

        // OrderId
        builder.Property(x => x.OrderId)
               .IsRequired();

        // UserId
        builder.Property(x => x.UserId)
               .HasMaxLength(450)
               .IsRequired();

        // Amount
        builder.Property(x => x.Amount)
               .HasPrecision(18, 2)
               .IsRequired();

        // Currency
        builder.Property(x => x.Currency)
               .HasMaxLength(10)
               .IsRequired();

        // Payment Method
        builder.Property(x => x.PaymentMethod)
               .HasMaxLength(50)
               .IsRequired();

        // Transaction Id
        builder.Property(x => x.TransactionId)
               .HasMaxLength(200);

        // Status
        builder.Property(x => x.Status)
               .HasMaxLength(30)
               .IsRequired();

        // Provider
        builder.Property(x => x.Provider)
               .HasMaxLength(50)
               .IsRequired();

        // Created At
        builder.Property(x => x.CreatedAt)
               .IsRequired();

        // Updated At
        builder.Property(x => x.UpdatedAt);

        // Indexes
        builder.HasIndex(x => x.TransactionId)
               .IsUnique();

        builder.HasIndex(x => x.OrderId);

        builder.HasIndex(x => x.UserId);
    }
}