using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        

        builder.Property(u => u.Username)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.PasswordHash)
               .IsRequired();

        builder.Property(u => u.Role)
               .IsRequired();

        builder.HasOne(u => u.Customer)
               .WithOne()
               .HasForeignKey<User>(u => u.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
