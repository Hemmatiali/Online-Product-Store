using CodeChallengeProject.Domain.Entities;
using CodeChallengeProject.Domain.ValueObjects;
using CodeChallengeProject.Persistence.Data.EntityConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallengeProject.Persistence.Data.EntityConfigurations;

/// <summary>
///     Represents the entity configuration for the <see cref="Product"/>> entity.
/// </summary>
public sealed class ProductConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        // Table Name
        builder.ToTable("Products");

        // Value Object Configurations
        // Title
        builder.OwnsOne(p => p.Title, titleBuilder =>
        {
            titleBuilder.Property(t => t.Value).HasColumnName("Title")
                .HasMaxLength(ProductTitleValueObject.MaximumLength)
                .IsUnicode(false) //Varchar
                .IsRequired();
            titleBuilder.HasIndex(e => e.Value).IsUnique();
        });

        // Inventory Count
        builder.OwnsOne(p => p.InventoryCount, inventoryBuilder =>
        {
            inventoryBuilder.Property(t => t.Value).HasColumnName("InventoryCount")
                .HasDefaultValueSql("0") //Predefined count. it depends on business logic.
                .IsRequired();
        });

        // Price
        builder.OwnsOne(p => p.Price, priceBuilder =>
        {
            priceBuilder.Property(t => t.Value).HasColumnName("Price")
                .HasDefaultValueSql("0")
                .IsRequired();
        });

        // Discount
        builder.OwnsOne(p => p.Discount, discountBuilder =>
        {
            discountBuilder.Property(t => t.Value).HasColumnName("Discount")
                .HasDefaultValueSql("0")
                .IsRequired();
        });
    }
}