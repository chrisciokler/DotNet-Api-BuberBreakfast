// Import the BuberBreakfast.Models namespace
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

// Declare the BuberBreakfast.Services.Breakfasts namespace
namespace BuberBreakfast.Services.Breakfasts
{
  // Define the BreakfastServices class which implements the IBreakfastService interface
  public class BreakfastService : IBreakfastService
  {
    // Declare a private dictionary to store breakfast objects, with the Guid as the key and Breakfast as the value
    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();

    // Define the CreateBreakfast method which takes a Breakfast object as a parameter
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
      // Add the breakfast object to the dictionary using the breakfast's Id as the key
      _breakfasts.Add(breakfast.Id, breakfast);
      return Result.Created;
    }

    // Define the GetBreakfast method which takes a Guid id as a parameter and returns a Breakfast object
    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
      // Return the Breakfast object corresponding to the provided id from the dictionary
      if (_breakfasts.TryGetValue(id, out var breakfast))
      {
        return _breakfasts[id];
      }

      return Errors.Breakfast.NotFound;
    }

    public ErrorOr<Breakfast[]> ListBreakfast()
    {
      // Return the Breakfast object corresponding to the provided id from the dictionary
      var arr = _breakfasts.Values.ToArray(); Breakfast[] breakfasts = _breakfasts.Values.ToArray();
      bool isArray = arr is Breakfast[];
      bool isArray2 = arr.GetType() == typeof(Breakfast[]);

      if (isArray && isArray2)
      {
        return _breakfasts.Values.ToArray();
      }

      return Errors.Breakfast.ListError;
    }

    public ErrorOr<Boolean> UpsertBreakfast(Breakfast breakfast)
    {

      bool isNew = _breakfasts.ContainsKey(breakfast.Id);

      _breakfasts[breakfast.Id] = breakfast;
      var bf = _breakfasts[breakfast.Id];

      if (bf == breakfast)
      {
        return !isNew;
      }

      return Errors.Breakfast.UpsertError;
    }

    public ErrorOr<Deleted> Delete(Guid id)
    {
      _breakfasts.Remove(id);

      bool check = _breakfasts.ContainsKey(id);

      if (check)
      {
        return Errors.Breakfast.DeleteError;
      }

      return Result.Deleted;
    }
  }
}
