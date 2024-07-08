using System.ComponentModel.DataAnnotations;
using CodeChallengeProject.Domain.ValueObjects;

namespace CodeChallengeProject.Application.ViewModels.Product;

/// <summary>
///     Represents the view model for increasing the inventory count of a product.
/// </summary>
public sealed class IncreaseInventoryViewModel
{
    [Required(ErrorMessage = "The product ID is required.")]
    public required int ProductId { get; set; }

    [Required(ErrorMessage = "The quantity is required.")]
    [Range(InventoryCountValueObject.MinimumCount, int.MaxValue - 1, ErrorMessage = "The product quantity must be between {1} and {2}.")]
    public required int Quantity { get; set; }
}