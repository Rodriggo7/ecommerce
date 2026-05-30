// OrderItemConfiguration.cs
namespace ECommerce.Infrastructure.Persistence.Configurations;

using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedNever();

        builder.Property(i => i.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");

        builder.Ignore(i => i.Subtotal);
    }
}