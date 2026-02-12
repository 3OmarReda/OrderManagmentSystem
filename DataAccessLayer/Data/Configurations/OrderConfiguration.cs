using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        

        builder.Property(o => o.TotalAmount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(o => o.Status)
               .IsRequired();

        builder.Property(o => o.PaymentMethod)
               .IsRequired();

        builder.Property(o => o.OrderDate)
               .IsRequired();

        builder.HasMany(o => o.OrderItems)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Invoice)
               .WithOne(i => i.Order)
               .HasForeignKey<Invoice>(i => i.OrderId);
    }
}
