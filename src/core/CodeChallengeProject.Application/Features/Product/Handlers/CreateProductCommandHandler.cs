using AutoMapper;
using CodeChallengeProject.Application.Features.Product.Commands;
using CodeChallengeProject.Domain.Models;
using CodeChallengeProject.Domain.Shared;
using CodeChallengeProject.Domain.Shared.StaticFunctions;
using CodeChallengeProject.Persistence.Data.Repositories.Interfaces;
using MediatR;

namespace CodeChallengeProject.Application.Features.Product.Handlers;

/// <summary>
///     Handles the creation of a new product.
/// </summary>
public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, OperationResultModel<int>>
{
    // Fields
    private readonly IBaseRepository<CodeChallengeProject.Domain.Entities.Product> _productBaseRepository;
    private readonly IMapper _mapper;

    // Ctor
    public CreateProductCommandHandler(IRepositoryFactory repositoryFactory, IMapper mapper)
    {
        _productBaseRepository = repositoryFactory.GetRepository<CodeChallengeProject.Domain.Entities.Product>();
        _mapper = mapper;
    }

    // Methods

    /// <summary>
    ///     Processes creates a new product.
    /// </summary>
    /// <param name="request">Command to create the product.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>ID of the created product.</returns>
    public async Task<OperationResultModel<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productViewModel = request.ProductViewModel;

        // Check Title to be uniqueness
        var isTitleExist = await _productBaseRepository.AnyAsync(p => p.Title.Value.Equals(productViewModel.Title.NormalizeAndTrim()));
        if (isTitleExist)
            return OperationResultModel<int>.Fail(StringStaticFunctions.FormatMessage(ErrorMessages.ExistMsg, "Product name"));

        // Map
        var product = _mapper.Map<CodeChallengeProject.Domain.Entities.Product>(productViewModel);
        if (product == null) return OperationResultModel<int>.Fail(StringStaticFunctions.FormatMessage(ErrorMessages.UnexpectedErrorMsg));

        // Add to db
        await _productBaseRepository.AddAsync(product);
        await _productBaseRepository.SaveChangesAsync();

        // Inserted product
        return OperationResultModel<int>.Success(data: product.Id);
    }
}