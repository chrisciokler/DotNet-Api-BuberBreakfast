// This controller handles the endpoints for managing breakfasts
namespace BuberBreakfast.Controllers;

// Importing the necessary namespaces
using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services.Breakfasts;
using BuberBreakfast.Services.Mapping;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

public class BreakfastsController : ApiController
{
  private readonly IBreakfastService _breakfastService;
  private readonly IAPICommunicationMapping _apiCommunicationMapping;
  // Constructor for injecting the breakfast service dependency
  public BreakfastsController(IBreakfastService breakfastService, IAPICommunicationMapping apiCommunicationMapping)
  {
    _breakfastService = breakfastService;
    _apiCommunicationMapping = apiCommunicationMapping;
  }

  // This endpoint allows creating a new breakfast
  [HttpPost()]
  public IActionResult CreateBreakfast(CreateBreakfastRequest request)
  {
    ErrorOr<Breakfast> breakfast = _apiCommunicationMapping.MapBreakfastRequest(request);

    if (breakfast.IsError)
    {
      return Problem(breakfast.Errors);
    }

    // Save breakfast to database
    _breakfastService.CreateBreakfast(breakfast.Value);

    // Mapping data from breakfasts to response
    var response = _apiCommunicationMapping.MapBreakfastResponse(breakfast.Value);

    // Return the created breakfast response and its endpoint for retrieval
    return CreatedAtAction(
      actionName: nameof(GetBreakfast),
      routeValues: new { id = breakfast.Value.Id },
      value: response
      );
  }

  // This endpoint allows retrieving a breakfast by its id
  [HttpGet("{id:guid}")]
  public IActionResult GetBreakfast(Guid id)
  {
    ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

    return getBreakfastResult.Match(
      breakfast => Ok(_apiCommunicationMapping.MapBreakfastResponse(breakfast)),
      errors => Problem(errors)
    );
  }

  [HttpGet()]
  public IActionResult ListBreakfast(Guid id)
  {
    ErrorOr<Breakfast[]> breakfasts = _breakfastService.ListBreakfast();

    return breakfasts.Match(
      breakfasts => Ok(breakfasts.Select(breakfast => _apiCommunicationMapping.MapBreakfastResponse(breakfast))),
      errors => Problem(errors)
    );

  }

  // This endpoint allows updating or inserting a breakfast using its id
  [HttpPut("{id:guid}")]
  public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
  {
    ErrorOr<Breakfast> breakfast = _apiCommunicationMapping.MapBreakfastUpsertRequest(id, request);

    if (breakfast.IsError)
    {
      return Problem(breakfast.Errors);
    }

    ErrorOr<Boolean> result = _breakfastService.UpsertBreakfast(breakfast.Value);

    return result.Match(
      isNew => isNew ? CreatedAtAction(
      actionName: nameof(GetBreakfast),
      routeValues: new { id = breakfast.Value.Id },
      value: _apiCommunicationMapping.MapBreakfastResponse(breakfast.Value)
      ) : Ok(_apiCommunicationMapping.MapBreakfastResponse(breakfast.Value)),
      errors => Problem(errors)
    );
  }

  // This endpoint allows deleting a breakfast by its id
  [HttpDelete("{id:guid}")]
  public IActionResult DeleteBreakfast(Guid id)
  {
    ErrorOr<Deleted> deletedResult = _breakfastService.Delete(id);

    return deletedResult.Match(
      deleted => NoContent(),
      errors => Problem(errors)
    );
  }

}
