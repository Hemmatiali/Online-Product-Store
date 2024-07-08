namespace CodeChallengeProject.Domain.Entities;

/// <summary>
///     Represents an order entity.
/// </summary>
public class Order : BaseEntity
{
    // Properties
    public int Product { get; private set; }
    public DateTime CreationDate { get; private set; }
    public int Buyer { get; private set; }

    // Constructors
    // Empty constructor for EF Core
    private Order()
    {
    }

    public Order(int product, int buyer)
    {
        Product = product;
        Buyer = buyer;
    }

    public Order(int product, DateTime creationDate, int buyer)
    {
        Product = product;
        CreationDate = creationDate;
        Buyer = buyer;
    }

    // Navigational properties 
    public virtual Product ProductEntity { get; set; }
    public virtual User BuyerEntity { get; set; }

    // Methods
}