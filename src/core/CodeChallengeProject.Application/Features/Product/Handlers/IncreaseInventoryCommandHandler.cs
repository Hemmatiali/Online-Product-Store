using AutoMapper;
using CodeChallengeProject.Application.Features.Product.Commands;
using CodeChallengeProject.Domain.Models;
using CodeChallengeProject.Domain.Shared;
using CodeChallengeProject.Domain.Shared.StaticFunctions;
using CodeChallengeProject.Domain.ValueObjects;
using CodeChallengeProject.Persistence.Data.Repositories.Interfaces;
using MediatR;

namespace CodeChallengeProject.Application.Features.Product.Handlers;

/// <summary>
///     Handles the IncreaseInventoryCommand to update a product's inventory.
/// </summary>
public sealed class IncreaseInventoryCommandHandler : IRequestHandler<IncreaseInventoryCommand, OperationResultModel>
{
    // Fields
    private readonly IBaseRepository<CodeChallengeProject.Domain.Entities.Product> _productBaseRepository;
    private readonly IMapper _mapper;

    // Ctor
    public IncreaseInventoryCommandHandler(IRepositoryFactory repositoryFactory, IMapper mapper)
    {
        _productBaseRepository = repositoryFactory.GetRepository<CodeChallengeProject.Domain.Entities.Product>();
        _mapper = mapper;
    }

    // Methods

    /// <summary>
    ///     Processes to increase the inventory of a product.
    /// </summary>
    /// <param name="request">The command containing the product ID and quantity to increase.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An OperationResultModel indicating success or failure of the inventory update.</returns>
    public async Task<OperationResultModel> Handle(IncreaseInventoryCommand request, CancellationToken cancellationToken)
    {
        var productViewModel = request.IncreaseInventoryViewModel;

        // Get product
        var product = await _productBaseRepository.GetByIdAsync(productViewModel.ProductId);

        // Check
        if (product == null)
            return OperationResultModel.Fail(StringStaticFunctions.FormatMessage(ErrorMessages.ItemNotFoundMsg, "Product"));

        // Update
        product.UpdateInventoryCount(new InventoryCountValueObject(productViewModel.Quantity + product.InventoryCount.Value));

        // Update db
        await _productBaseRepository.UpdateAsync(product);
        await _productBaseRepository.SaveChangesAsync();

        // Done
        return OperationResultModel.Success();
    }
}