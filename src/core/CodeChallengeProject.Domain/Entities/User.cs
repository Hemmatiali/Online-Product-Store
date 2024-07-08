using CodeChallengeProject.Domain.ValueObjects;

namespace CodeChallengeProject.Domain.Entities;

/// <summary>
///     Represents a user entity.
/// </summary>
public class User : BaseEntity
{
    // Properties
    public NameUserValueObject Name { get; private set; }

    // Constructors
    // Empty constructor for EF Core
    private User()
    {
    }

    public User(int id, NameUserValueObject name)
    {
        Id = id;
        Name = name;
    }

    // Navigational properties 
    public virtual ICollection<Order> Orders { get; set; }

    // Methods
}