// This namespace contains the models for the BuberBreakfast application
using BuberBreakfast.Contracts.Breakfast;
using ErrorOr;

namespace BuberBreakfast.Models;

// This class represents a Breakfast object
public class Breakfast
{
  public const int MinNameLenght = 3;
  public const int MaxNameLenght = 50;
  public Guid Id { get; }
  public string Name { get; }
  public string Description { get; }
  public DateTime StartDateTime { get; }
  public DateTime EndDateTime { get; }
  public DateTime LastModifiedDateTime { get; }
  public List<string> Savory { get; }
  public List<string> Sweet { get; }
  private Breakfast(Guid id, string name, string description, DateTime startDateTime, DateTime endDateTime, DateTime lastModifiedDateTime, List<string> savory, List<string> sweet)
  {
    Id = id;
    Name = name;
    Description = description;
    StartDateTime = startDateTime;
    EndDateTime = endDateTime;
    LastModifiedDateTime = lastModifiedDateTime;
    Savory = savory;
    Sweet = sweet;
  }

  public static ErrorOr<Breakfast> Create(CreateBreakfastRequest request)
  {
    if (request.Name.Length is < MinNameLenght or > MaxNameLenght)
    {
      return ServiceErrors.Errors.Breakfast.InvalidName;
    }

    return new Breakfast(
        Guid.NewGuid(),
        request.Name,
        request.Description,
        request.StartDateTime,
        request.EndDateTime,
        request.LastModifiedDateTime,
        request.Savory,
        request.Sweet
      );
  }
  public static ErrorOr<Breakfast> Upsert(Guid id, UpsertBreakfastRequest request)
  {
    if (request.Name.Length is < MinNameLenght or > MaxNameLenght)
    {
      return ServiceErrors.Errors.Breakfast.InvalidName;
    }

    return new Breakfast(
        id,
        request.Name,
        request.Description,
        request.StartDateTime,
        request.EndDateTime,
        request.LastModifiedDateTime,
        request.Savory,
        request.Sweet
      );
  }
}
