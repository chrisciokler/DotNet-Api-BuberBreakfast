using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Mapping
{
  public interface IAPICommunicationMapping
  {
    ErrorOr<Breakfast> MapBreakfastRequest(CreateBreakfastRequest request);
    ErrorOr<Breakfast> MapBreakfastUpsertRequest(Guid id, UpsertBreakfastRequest request);
    BreakfastResponse MapBreakfastResponse(Breakfast breakfast);
  }
}