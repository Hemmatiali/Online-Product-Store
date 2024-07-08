using AutoMapper;
using CodeChallengeProject.Application.Features.Product.DTOs;
using CodeChallengeProject.Application.Features.Product.Queries;
using CodeChallengeProject.Domain.Models;
using CodeChallengeProject.Domain.Shared;
using CodeChallengeProject.Domain.Shared.StaticFunctions;
using CodeChallengeProject.Persistence.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace CodeChallengeProject.Application.Features.Product.Handlers;

/// <summary>
///     Handles the retrieval of a product by ID.
/// </summary>
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, OperationResultModel<ProductDto?>>
{
    // Fields
    private readonly IBaseRepository<CodeChallengeProject.Domain.Entities.Product> _baseRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;


    // Ctor
    public GetProductByIdQueryHandler(IRepositoryFactory repositoryFactory, IMapper mapper, IMemoryCache memoryCache)
    {
        _baseRepository = repositoryFactory.GetRepository<CodeChallengeProject.Domain.Entities.Product>();
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    //Methods

    /// <summary>
    ///  Processes retrieves product information by ID.
    /// </summary>
    /// <param name="request">The GetProductByIdQuery request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Operation result containing product details if found.</returns>
    public async Task<OperationResultModel<ProductDto?>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        // Get product from cache
        var product = await _memoryCache.GetOrCreateAsync(
            $"product_{request.Id}", // Cache key
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return await _baseRepository.GetByIdAsync(request.Id);
            });

        // Check
        if (product == null)
            return OperationResultModel<ProductDto?>.Fail(StringStaticFunctions.FormatMessage(ErrorMessages.ItemNotFoundMsg, "Product"));

        // Map
        var productDto = new ProductDto()
        {
            Id = product.Id,
            Title = product.Title.Value,
            InventoryCount = product.InventoryCount.Value,
            Discount = product.Discount.Value,
            Price = product.Price.Value - product.Discount.CalculateDiscountAmount(product.Price.Value)
        };

        return OperationResultModel<ProductDto?>.Success(data: productDto);
    }
}