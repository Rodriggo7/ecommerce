// OrderConfiguration.cs
namespace ECommerce.Infrastructure.Persistence.Configurations;

using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedNever();

        builder.Property(o => o.Total).HasColumnType("decimal(18,2)");
        
        builder.Property(o => o.Status).IsRequired().HasConversion<string>(); 
        
        builder.HasMany(o => o.Items)
               .WithOne()
               .HasForeignKey(i => i.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(o => o.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}