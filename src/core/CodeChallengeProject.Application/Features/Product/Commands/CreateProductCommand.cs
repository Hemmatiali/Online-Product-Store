using CodeChallengeProject.Application.ViewModels.Product;
using CodeChallengeProject.Domain.Models;
using MediatR;

namespace CodeChallengeProject.Application.Features.Product.Commands;

/// <summary>
///     Command to create a new product.
/// </summary>
public sealed class CreateProductCommand(CreateProductViewModel productViewModel) : IRequest<OperationResultModel<int>> // Returns the ID of the created product 
{
    public CreateProductViewModel ProductViewModel { get; set; } = productViewModel;
}