using System.ComponentModel.DataAnnotations;
using CodeChallengeProject.Domain.ValueObjects;

namespace CodeChallengeProject.Application.ViewModels.Product;

/// <summary>
///     Represents the view model for creating a product.
/// </summary>
public sealed class CreateProductViewModel
{
    [Required(ErrorMessage = "The product title is required.")]
    [MaxLength(40, ErrorMessage = "The product title must be a maximum of 40 characters.")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "The product price is required.")]
    [Range(PriceValueObject.MinimumPrice, PriceValueObject.MaximumPrice, ErrorMessage = "The product price must be between {1} and {2}.")]
    public required int Price { get; set; }

    [Range(DiscountValueObject.MinimumDiscount, DiscountValueObject.MaximumDiscount, ErrorMessage = "The product discount must be between {1} and {2}.")]
    public int Discount { get; set; }
}