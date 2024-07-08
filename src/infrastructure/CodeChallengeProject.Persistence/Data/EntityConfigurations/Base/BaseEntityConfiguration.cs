using CodeChallengeProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallengeProject.Persistence.Data.EntityConfigurations.Base;

/// <summary>
///     Represents the abstract entity configuration for the <see cref="BaseEntity"/>> entity.
/// </summary>
public class BaseEntityConfiguration<TEntityType> : IEntityTypeConfiguration<TEntityType> where TEntityType : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntityType> builder)
    {
        // Primary Key
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
    }
}

