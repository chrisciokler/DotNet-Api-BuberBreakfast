using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Mapping
{
  public class APICommunicationMapping : IAPICommunicationMapping
  {
    public ErrorOr<Breakfast> MapBreakfastRequest(CreateBreakfastRequest request)
    {
      return Breakfast.Create(request);
    }

    public BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
      return new BreakfastResponse(
        breakfast.Id,
        breakfast.Name,
        breakfast.Description,
        breakfast.StartDateTime,
        breakfast.EndDateTime,
        breakfast.LastModifiedDateTime,
        breakfast.Savory,
        breakfast.Sweet
      );
    }

    public ErrorOr<Breakfast> MapBreakfastUpsertRequest(Guid id, UpsertBreakfastRequest request)
    {
      return Breakfast.Upsert(id, request);
    }
  }
}