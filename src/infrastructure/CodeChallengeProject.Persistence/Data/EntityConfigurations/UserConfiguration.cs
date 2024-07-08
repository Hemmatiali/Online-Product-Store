using CodeChallengeProject.Domain.Entities;
using CodeChallengeProject.Domain.ValueObjects;
using CodeChallengeProject.Persistence.Data.EntityConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallengeProject.Persistence.Data.EntityConfigurations;

/// <summary>
///     Represents the entity configuration for the <see cref="User"/>> entity.
/// </summary>
public sealed class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        // Table Name
        builder.ToTable("Users");

        // Value Object Configurations
        // Name
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new NameUserValueObject(x))
            .HasColumnName("Name")
            .HasMaxLength(NameUserValueObject.MaximumLength)
            .IsUnicode(false) //Varchar 
            .IsRequired();

        // Seed data
        builder.HasData(
            new User(id: 1, name: new NameUserValueObject("Ali")),
            new User(id: 2, name: new NameUserValueObject("Reza")),
            new User(id: 3, name: new NameUserValueObject("Hosein")),
            new User(id: 4, name: new NameUserValueObject("Hamed")),
            new User(id: 5, name: new NameUserValueObject("Farzad"))
        );
    }
}