using CodeChallengeProject.Application.Features.Order.Commands;
using CodeChallengeProject.Domain.Entities;
using CodeChallengeProject.Domain.Models;
using CodeChallengeProject.Domain.Shared;
using CodeChallengeProject.Domain.Shared.StaticFunctions;
using CodeChallengeProject.Domain.ValueObjects;
using CodeChallengeProject.Persistence.Data.Repositories.Interfaces;
using MediatR;

namespace CodeChallengeProject.Application.Features.Order.Handlers;

/// <summary>
///     Handles the purchase a product process through a user.
/// </summary>
public sealed class BuyProductCommandHandler : IRequestHandler<BuyProductCommand, OperationResultModel>
{
    // Fields
    private readonly IBaseRepository<CodeChallengeProject.Domain.Entities.Product> _productBaseRepository;
    private readonly IBaseRepository<User> _userBaseRepository;
    private readonly IBaseRepository<CodeChallengeProject.Domain.Entities.Order> _ordBaseRepository;

    // Ctor
    public BuyProductCommandHandler(IRepositoryFactory repositoryFactory)
    {
        _productBaseRepository = repositoryFactory.GetRepository<CodeChallengeProject.Domain.Entities.Product>();
        _userBaseRepository = repositoryFactory.GetRepository<User>();
        _ordBaseRepository = repositoryFactory.GetRepository<CodeChallengeProject.Domain.Entities.Order>();
    }

    // Methods

    /// <summary>
    ///     Processes a product purchase and creates an order.
    /// </summary>
    /// <param name="request">The BuyProductCommand request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An OperationResultModel indicating success or failure.</returns>
    public async Task<OperationResultModel> Handle(BuyProductCommand request, CancellationToken cancellationToken) 
    {
        var buyProductViewModel = request.BuyProductViewModel;

        // Get product
        var product = await _productBaseRepository.GetByIdAsync(buyProductViewModel.ProductId);
        if (product == null)
            return OperationResultModel.Fail(StringStaticFunctions.FormatMessage(ErrorMessages.ItemNotFoundMsg, "Product"));

        // Get user
        var user = await _userBaseRepository.GetByIdAsync(buyProductViewModel.UserId);
        if (user == null)
            return OperationResultModel.Fail(StringStaticFunctions.FormatMessage(ErrorMessages.ItemNotFoundMsg, "User"));

        // Check product inventory count
        if (product.InventoryCount.Value <= 0)
            return OperationResultModel.Fail(StringStaticFunctions.FormatMessage(ErrorMessages.CanNotBeNegativeMsg, "Inventory count"));

        // Check if the user already has an open order for this product
        var isOrderExist = await _ordBaseRepository.AnyAsync(o => o.Buyer == user.Id && o.Product == product.Id);
        if (isOrderExist)
            return OperationResultModel.Fail("You already have an open order for this product.");

        // Create new order and update inventory atomically 
        await using var transaction = await _ordBaseRepository.BeginTransactionAsync(cancellationToken);
        try
        {
            // Update product
            product.UpdateInventoryCount(new InventoryCountValueObject(product.InventoryCount.Value - 1));
            await _productBaseRepository.UpdateAsync(product);

            // Update order
            var order = new CodeChallengeProject.Domain.Entities.Order(product: product.Id, buyer: user.Id);
            await _ordBaseRepository.AddAsync(order);
            await _ordBaseRepository.SaveChangesAsync();
            await transaction.CommitAsync(cancellationToken);

            return OperationResultModel.Success();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}