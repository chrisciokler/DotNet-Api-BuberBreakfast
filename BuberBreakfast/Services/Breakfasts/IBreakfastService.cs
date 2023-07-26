// Import the necessary models
using BuberBreakfast.Models;
using ErrorOr;

// Set the namespace for the Breakfast service
namespace BuberBreakfast.Services.Breakfasts
{
  // Define the interface for the Breakfast service
  public interface IBreakfastService
  {
    // Method to create a new breakfast
    // Takes in a Breakfast object as a parameter
    ErrorOr<Created> CreateBreakfast(Breakfast breakfast);

    ErrorOr<Breakfast[]> ListBreakfast();
    ErrorOr<Breakfast> GetBreakfast(Guid id);
    ErrorOr<Boolean> UpsertBreakfast(Breakfast breakfast);
    ErrorOr<Deleted> Delete(Guid id);
  }
}