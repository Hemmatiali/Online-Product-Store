using CodeChallengeProject.Domain.Entities;
using CodeChallengeProject.Persistence.Data.EntityConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallengeProject.Persistence.Data.EntityConfigurations;

/// <summary>
///     Represents the entity configuration for the <see cref="Order"/>> entity.
/// </summary>
public sealed class OrderConfiguration : BaseEntityConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        // Table Name
        builder.ToTable("Orders");

        // CreationDate
        builder.Property(p => p.CreationDate)
            .HasDefaultValueSql("GETDATE()") // Default value to current date time
            .IsRequired();

        // Navigation
        builder.HasOne(d => d.ProductEntity).WithMany(p => p.Orders)
            .HasForeignKey(d => d.Product)
            .OnDelete(DeleteBehavior.ClientNoAction)
            .HasConstraintName("FK_Orders_Products_ProductId");

        builder.HasOne(d => d.BuyerEntity).WithMany(p => p.Orders)
            .HasForeignKey(d => d.Buyer)
            .OnDelete(DeleteBehavior.ClientNoAction)
            .HasConstraintName("FK_Orders_Users_BuyerId");
    }
}