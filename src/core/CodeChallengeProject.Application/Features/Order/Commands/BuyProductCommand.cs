using CodeChallengeProject.Application.ViewModels.Order;
using CodeChallengeProject.Domain.Models;
using MediatR;

namespace CodeChallengeProject.Application.Features.Order.Commands;

/// <summary>
///     Command to buy a product from a user.
/// </summary>
public sealed class BuyProductCommand(BuyProductViewModel buyProductViewModel) : IRequest<OperationResultModel>
{
    public BuyProductViewModel BuyProductViewModel { get; set; } = buyProductViewModel;
}