using CodeChallengeProject.Application.Features.Product.DTOs;
using CodeChallengeProject.Domain.Models;
using MediatR;

namespace CodeChallengeProject.Application.Features.Product.Queries;

/// <summary>
///     Represents a query to retrieve a product by ID.
/// </summary>
public sealed class GetProductByIdQuery : IRequest<OperationResultModel<ProductDto?>>
{
    public int Id { get; set; }

    public GetProductByIdQuery(int id)
    {
        Id = id;
    }
}