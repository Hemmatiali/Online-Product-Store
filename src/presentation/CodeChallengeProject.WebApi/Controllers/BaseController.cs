using Microsoft.AspNetCore.Mvc;

namespace CodeChallengeProject.WebApi.Controllers;

/// <summary>
///     Base controller for API endpoints.
/// </summary>
[ApiController]
[Route("api/v1/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
}