namespace CodeChallengeProject.Application.Features.Product.DTOs;

/// <summary>
///     Data transfer object (DTO) representing a product.
/// </summary>
public sealed class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int InventoryCount { get; set; }
    public int Price { get; set; }
    public int Discount { get; set; }
}