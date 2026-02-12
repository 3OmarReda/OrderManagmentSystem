using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        

        builder.Property(i => i.TotalAmount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(i => i.InvoiceDate)
               .IsRequired();

        builder.HasOne(i => i.Order)
               .WithOne(o => o.Invoice)
               .HasForeignKey<Invoice>(i => i.OrderId);
    }
}
