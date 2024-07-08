using AutoMapper;
using Moq;
using CodeChallengeProject.Application.Features.Product.Commands;
using CodeChallengeProject.Application.Features.Product.Handlers;
using CodeChallengeProject.Application.ViewModels.Product;
using CodeChallengeProject.Domain.Shared.StaticFunctions;
using CodeChallengeProject.Domain.Shared;
using CodeChallengeProject.Domain.ValueObjects;
using CodeChallengeProject.Persistence.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace CodeChallenge.UnitTests.Features.Product.Handlers;

/// <summary>
///     Unit tests for the <see cref="CreateProductCommandHandler"/> class.
/// </summary>
public sealed class CreateProductCommandHandlerTests
{
    // Mocks
    private readonly Mock<IRepositoryFactory> _repositoryFactoryMock;
    private readonly Mock<IBaseRepository<CodeChallengeProject.Domain.Entities.Product>> _productBaseRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateProductCommandHandler _handler;

    // Setup
    public CreateProductCommandHandlerTests()
    {
        _repositoryFactoryMock = new Mock<IRepositoryFactory>();
        _productBaseRepositoryMock = new Mock<IBaseRepository<CodeChallengeProject.Domain.Entities.Product>>();
        _mapperMock = new Mock<IMapper>();

        _repositoryFactoryMock.Setup(f => f.GetRepository<CodeChallengeProject.Domain.Entities.Product>()).Returns(_productBaseRepositoryMock.Object);

        _handler = new CreateProductCommandHandler(_repositoryFactoryMock.Object, _mapperMock.Object);
    }

    #region Tests

    [Fact]
    public async Task Handle_UniqueTitle_CreatesProduct()
    {
        // Arrange
        var viewModel = new CreateProductViewModel
        {
            Title = "Product 1",
            Price = 100,
            Discount = 10
        };

        var expectedProduct = new CodeChallengeProject.Domain.Entities.Product(
            new ProductTitleValueObject(viewModel.Title),
            new PriceValueObject(viewModel.Price),
            new DiscountValueObject(viewModel.Discount)
        );

        _mapperMock.Setup(mapper => mapper.Map<CodeChallengeProject.Domain.Entities.Product>(viewModel)).Returns(expectedProduct);
        _productBaseRepositoryMock.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<CodeChallengeProject.Domain.Entities.Product, bool>>>()))
            .ReturnsAsync(false); // No existing product with the same title

        // Act
        var result = await _handler.Handle(new CreateProductCommand(viewModel), CancellationToken.None);

        // Assert
        Assert.True(result.WasSuccess);
        Assert.Equal(expectedProduct.Id, result.Data); // Assuming Id is assigned after saving
        _productBaseRepositoryMock.Verify(repo => repo.AddAsync(expectedProduct), Times.Once);
        _productBaseRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_DuplicateTitle_ReturnsFailure()
    {
        // Arrange
        var viewModel = new CreateProductViewModel
        {
            Title = "Product 1",
            Price = 100,
            Discount = 10
        };

        _productBaseRepositoryMock.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<CodeChallengeProject.Domain.Entities.Product, bool>>>()))
            .ReturnsAsync(true); // A product with the same title already exists

        // Act
        var result = await _handler.Handle(new CreateProductCommand(viewModel), CancellationToken.None);

        // Assert
        Assert.False(result.WasSuccess);
        Assert.Equal(StringStaticFunctions.FormatMessage(ErrorMessages.ExistMsg, "Product name"), result.ErrorMessage);
        _productBaseRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<CodeChallengeProject.Domain.Entities.Product>()), Times.Never); // Ensure product is not added
        _productBaseRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Never); // Ensure changes are not saved
    }

    #endregion

}