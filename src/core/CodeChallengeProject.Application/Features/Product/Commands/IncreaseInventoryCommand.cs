using CodeChallengeProject.Application.ViewModels.Product;
using CodeChallengeProject.Domain.Models;
using MediatR;

namespace CodeChallengeProject.Application.Features.Product.Commands;

/// <summary>
///     Command to increase inventory of a product.
/// </summary>
public sealed class IncreaseInventoryCommand(IncreaseInventoryViewModel increaseInventoryViewModel) : IRequest<OperationResultModel>
{
    public IncreaseInventoryViewModel IncreaseInventoryViewModel { get; set; } = increaseInventoryViewModel;
}