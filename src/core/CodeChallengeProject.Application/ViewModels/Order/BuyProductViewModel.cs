using System.ComponentModel.DataAnnotations;

namespace CodeChallengeProject.Application.ViewModels.Order;

/// <summary>
///     Represents the view model for buy a product.
/// </summary>
public sealed class BuyProductViewModel
{
    [Required(ErrorMessage = "The product ID is required.")]
    public required int ProductId { get; set; }

    [Required(ErrorMessage = "The user ID is required.")]
    public required int UserId { get; set; }
}