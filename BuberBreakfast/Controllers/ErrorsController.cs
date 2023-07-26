// This controller handles the endpoints for managing breakfasts
namespace BuberBreakfast.Controllers;

// Importing the necessary namespaces
using Microsoft.AspNetCore.Mvc;

public class ErrorsController : ControllerBase
{
  [Route("/error")]
  public IActionResult Error()
  {
    return Problem();
  }
}

// This section is for test code