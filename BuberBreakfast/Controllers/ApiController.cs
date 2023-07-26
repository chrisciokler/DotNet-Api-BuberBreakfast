// This controller handles the endpoints for managing breakfasts
namespace BuberBreakfast.Controllers;

using ErrorOr;

// Importing the necessary namespaces
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")] // This adds the route {domain}/breakfasts/:endpoints
public class ApiController : ControllerBase
{
  protected IActionResult Problem(List<Error> errors)
  {
    var firstError = errors[0];
    var statusCode = firstError.Type switch
    {
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      _ => StatusCodes.Status500InternalServerError
    };

    return Problem(statusCode: statusCode, title: firstError.Description);
  }
}
