using CodeChallengeProject.Domain.ValueObjects;

namespace CodeChallengeProject.Domain.Entities;

/// <summary>
///     Represents a product entity.
/// </summary>
public class Product : BaseEntity
{
    // Properties
    public ProductTitleValueObject Title { get; private set; }
    public InventoryCountValueObject InventoryCount { get; private set; }
    public PriceValueObject Price { get; private set; }
    public DiscountValueObject Discount { get; private set; }

    // Constructors
    // Empty constructor for EF
    private Product() { }

    public Product(ProductTitleValueObject title, PriceValueObject price, DiscountValueObject discount)
    {
        Title = title;
        Price = price;
        Discount = discount;
    }

    public Product(int id, ProductTitleValueObject title, InventoryCountValueObject inventoryCount, PriceValueObject price, DiscountValueObject discount)
    {
        Id = id;
        Title = title;
        InventoryCount = inventoryCount;
        Price = price;
        Discount = discount;
    }

    // Navigational properties 
    public virtual ICollection<Order> Orders { get; set; }


    // Methods

    /// <summary>
    ///     Updates the inventory count by increasing or decreasing it.
    /// </summary>
    /// <param name="inventoryCountValueObject">The value object representing the inventory count change.</param>
    public void UpdateInventoryCount(InventoryCountValueObject inventoryCountValueObject)
    {
        this.InventoryCount = inventoryCountValueObject;
    }
}