using Microsoft.EntityFrameworkCore.Storage;
using CodeChallengeProject.Application.Features.Order.Handlers;
using CodeChallengeProject.Persistence.Data.Repositories.Interfaces;
using System.Linq.Expressions;
using Moq;
using CodeChallengeProject.Application.Features.Order.Commands;
using CodeChallengeProject.Application.ViewModels.Order;
using CodeChallengeProject.Domain.Entities;
using CodeChallengeProject.Domain.ValueObjects;
using CodeChallengeProject.Domain.Shared.StaticFunctions;
using CodeChallengeProject.Domain.Shared;

namespace CodeChallenge.UnitTests.Features.Order.Handlers;

/// <summary>
///     Unit tests for the <see cref="BuyProductCommandHandler"/> class.
/// </summary>
public sealed class BuyProductCommandHandlerTests
{
    // Mocks
    private readonly Mock<IRepositoryFactory> _repositoryFactoryMock;
    private readonly Mock<IBaseRepository<CodeChallengeProject.Domain.Entities.Product>> _productBaseRepositoryMock;
    private readonly Mock<IBaseRepository<CodeChallengeProject.Domain.Entities.User>> _userBaseRepositoryMock;
    private readonly Mock<IBaseRepository<CodeChallengeProject.Domain.Entities.Order>> _orderBaseRepositoryMock;
    private readonly Mock<IDbContextTransaction> _transactionMock;
    private readonly BuyProductCommandHandler _handler;

    // Setup
    public BuyProductCommandHandlerTests()
    {
        _repositoryFactoryMock = new Mock<IRepositoryFactory>();
        _productBaseRepositoryMock = new Mock<IBaseRepository<CodeChallengeProject.Domain.Entities.Product>>();
        _userBaseRepositoryMock = new Mock<IBaseRepository<User>>();
        _orderBaseRepositoryMock = new Mock<IBaseRepository<CodeChallengeProject.Domain.Entities.Order>>();
        _transactionMock = new Mock<IDbContextTransaction>();

        _repositoryFactoryMock.Setup(f => f.GetRepository<CodeChallengeProject.Domain.Entities.Product>()).Returns(_productBaseRepositoryMock.Object);
        _repositoryFactoryMock.Setup(f => f.GetRepository<User>()).Returns(_userBaseRepositoryMock.Object);
        _repositoryFactoryMock.Setup(f => f.GetRepository<CodeChallengeProject.Domain.Entities.Order>()).Returns(_orderBaseRepositoryMock.Object);

        _handler = new BuyProductCommandHandler(_repositoryFactoryMock.Object);

        _orderBaseRepositoryMock.Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_transactionMock.Object);
    }

    #region Tests

    [Fact]
    public async Task Handle_SufficientInventory_CreatesOrder()
    {
        // Arrange
        const int productId = 1;
        const int userId = 1;

        var product = new CodeChallengeProject.Domain.Entities.Product
        (
            id: 1,
            new ProductTitleValueObject("Product 1"),
            new InventoryCountValueObject(10),
            new PriceValueObject(98000),
            new DiscountValueObject(10)
        );

        var user = new CodeChallengeProject.Domain.Entities.User(id: 1, new NameUserValueObject("Ali"));

        _productBaseRepositoryMock
            .Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(product);

        _userBaseRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync(user);

        _orderBaseRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<CodeChallengeProject.Domain.Entities.Order, bool>>>())).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(new BuyProductCommand(new BuyProductViewModel() { ProductId = productId, UserId = userId }), CancellationToken.None);

        // Assert
        Assert.True(result.WasSuccess);
        _productBaseRepositoryMock.Verify(repo => repo.UpdateAsync(product), Times.Once);
        _orderBaseRepositoryMock.Verify(repo => repo.AddAsync(It.Is<CodeChallengeProject.Domain.Entities.Order>(o => o.Product == productId && o.Buyer == userId)), Times.Once);
        _transactionMock.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InsufficientInventory_Fails()
    {
        // Arrange
        const int productId = 1;
        const int userId = 1;

        var product = new CodeChallengeProject.Domain.Entities.Product
        (
            id: 1,
            new ProductTitleValueObject("Product 1"),
            new InventoryCountValueObject(0),
            new PriceValueObject(98000),
            new DiscountValueObject(10)
        );

        var user = new CodeChallengeProject.Domain.Entities.User(id: 1, new NameUserValueObject("Ali"));

        _productBaseRepositoryMock
            .Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(product);

        _userBaseRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(new BuyProductCommand(new BuyProductViewModel() { ProductId = productId, UserId = userId }), CancellationToken.None);

        // Assert
        Assert.False(result.WasSuccess);
        Assert.Equal(StringStaticFunctions.FormatMessage(ErrorMessages.CanNotBeNegativeMsg, "Inventory count"), result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_DuplicateOrder_Fails()
    {
        // Arrange
        const int productId = 1;
        const int userId = 1;

        var product = new CodeChallengeProject.Domain.Entities.Product
        (
            id: 1,
            new ProductTitleValueObject("Product 1"),
            new InventoryCountValueObject(10),
            new PriceValueObject(98000),
            new DiscountValueObject(10)
        );

        var user = new CodeChallengeProject.Domain.Entities.User(id: 1, new NameUserValueObject("Ali"));

        _productBaseRepositoryMock
            .Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(product);

        _userBaseRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync(user);

        _orderBaseRepositoryMock
            .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<CodeChallengeProject.Domain.Entities.Order, bool>>>()))
            .ReturnsAsync(true); // Assuming an existing order exists

        // Act
        var result = await _handler.Handle(new BuyProductCommand(new BuyProductViewModel() { ProductId = productId, UserId = userId }), CancellationToken.None);

        // Assert
        Assert.False(result.WasSuccess);
        Assert.Equal("You already have an open order for this product.", result.ErrorMessage);
    }

    #endregion

}