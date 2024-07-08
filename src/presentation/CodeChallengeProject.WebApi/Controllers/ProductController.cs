using CodeChallengeProject.Application.Features.Order.Commands;
using CodeChallengeProject.Application.Features.Product.Commands;
using CodeChallengeProject.Application.Features.Product.Queries;
using CodeChallengeProject.Application.ViewModels.Order;
using CodeChallengeProject.Application.ViewModels.Product;
using CodeChallengeProject.Domain.Shared.StaticFunctions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallengeProject.WebApi.Controllers;

/// <summary>
///     Controller for managing products.
/// </summary>
public sealed class ProductController : BaseController
{
    #region Fields

    private readonly IMediator _mediator;
    private readonly ILogger<ProductController> _logger;

    #endregion

    #region Ctor

    public ProductController(IMediator mediator, ILogger<ProductController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Gets a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>The product details if found.</returns>
    /// <response code="200">Returns the product details.</response>
    /// <response code="404">If the product with the given ID is not found.</response>
    /// <response code="500">If an unexpected error occurs while retrieving the product.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            // Log request
            _logger.LogInformation($"Getting product with ID {id}");

            // Create and send the query
            var query = new GetProductByIdQuery(id);
            var getProductResult = await _mediator.Send(query);

            if (!getProductResult.WasSuccess)
            {
                // Log response
                _logger.LogWarning($"Product with ID {id} not found.");
                return NotFound();
            }

            // Log response
            _logger.LogInformation($"Retrieved product with ID {id} successfully.");

            // Return the product DTO
            return Ok(getProductResult.Data);
        }
        catch (Exception ex)
        {
            // Log exception
            _logger.LogError(ex, $"An error occurred while getting product with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while getting product with ID {id}.");
        }
    }

    /// <summary>
    ///     Creates a new product.
    /// </summary>
    /// <param name="viewModel">The view model containing information about the product to create.</param>
    /// <returns>The ID of the created product if successful.</returns>
    /// <response code="200">Returns the ID of the created product.</response>
    /// <response code="400">If the request contains invalid data.</response>
    /// <response code="500">If an unexpected error occurs while creating the product.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> CreateProduct(CreateProductViewModel viewModel)
    {
        try
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Log request
            _logger.LogInformation("Creating a new product.");

            // Send create product command
            var command = new CreateProductCommand(viewModel);
            var createdProductResult = await _mediator.Send(command);

            // Check if product creation was successful
            if (!createdProductResult.WasSuccess)
            {
                // Log response
                _logger.LogWarning("Failed to create product.");
                return Problem(detail: createdProductResult.ErrorMessage);
            }

            // Log response
            _logger.LogInformation($"Product created successfully. ID: {createdProductResult.Data}");

            // Done
            return Ok();
        }
        catch (Exception ex)
        {
            // Log exception
            _logger.LogError(ex, "An error occurred while creating the product.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while creating the product.");
        }
    }

    /// <summary>
    ///     Increases the inventory for a product.
    /// </summary>
    /// <param name="viewModel">The view model containing information about the inventory increase.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">Indicates successful increase of inventory.</response>
    /// <response code="400">If the request contains invalid data.</response>
    /// <response code="500">If an unexpected error occurs while increasing inventory.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> IncreaseInventory(IncreaseInventoryViewModel viewModel)
    {
        try
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Log request
            _logger.LogInformation(StringStaticFunctions.FormatMessage("Increasing inventory for product with ID: {0}, by {1}.", viewModel.ProductId, viewModel.Quantity));

            // Send increase inventory command
            var command = new IncreaseInventoryCommand(viewModel);
            var increaseInventoryIResult = await _mediator.Send(command);

            // Check if increase inventory was successful
            if (!increaseInventoryIResult.WasSuccess)
            {
                // Log response
                _logger.LogWarning("Failed to increase inventory of the product.");
                return Problem(detail: increaseInventoryIResult.ErrorMessage);
            }

            // Log response
            _logger.LogInformation(StringStaticFunctions.FormatMessage("Inventory increased successfully for product with ID: {0}.", viewModel.ProductId));

            // Done
            return NoContent();//204
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while increasing inventory for product.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while increasing inventory.");
        }
    }

    /// <summary>
    ///     Buys a product.
    /// </summary>
    /// <param name="viewModel">The view model containing information about the product purchase.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="200">Indicates successful purchase of the product.</response>
    /// <response code="400">If the request contains invalid data.</response>
    /// <response code="500">If an unexpected error occurs while processing the purchase.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BuyProduct(BuyProductViewModel viewModel)
    {
        try
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Log request
            _logger.LogInformation(StringStaticFunctions.FormatMessage("User with ID: {0} is buying product with ID: {1}.", viewModel.UserId, viewModel.ProductId));

            // Send buy product command
            var command = new BuyProductCommand(viewModel);
            var buyResult = await _mediator.Send(command);

            // Check if purchase process was successful
            if (!buyResult.WasSuccess)
            {
                // Log response
                _logger.LogWarning("Failed to buy the product.");
                return Problem(detail: buyResult.ErrorMessage);
            }

            // Log response
            _logger.LogInformation(StringStaticFunctions.FormatMessage("Product with ID: {0} purchased successfully by user with ID: {1}.", viewModel.ProductId, viewModel.UserId));

            // Done
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, StringStaticFunctions.FormatMessage("An error occurred while user with ID: {0} was buying product with ID: {1}.", viewModel.UserId, viewModel.ProductId));
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing the purchase.");
        }
    }

    #endregion
}