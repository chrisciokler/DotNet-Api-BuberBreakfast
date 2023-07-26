using ErrorOr;

namespace BuberBreakfast.ServiceErrors;

public static class Errors
{
  public static class Breakfast
  {
    public static Error NotFound => Error.NotFound(
      code: "Breakfast.NotFound",
      description: "Breakfast not found."
    );
    public static Error UpsertError => Error.Failure(
      code: "Breakfast.UpsertError",
      description: "Upsert failed."
    );
    public static Error ListError => Error.Failure(
      code: "Breakfast.ListError",
      description: "List failed."
    );
    public static Error DeleteError => Error.Failure(
      code: "Breakfast.DeleteError",
      description: "Item not deleted."
    );
    public static Error InvalidName => Error.Validation(
      code: "Breakfast.InvalidName",
      description: $"Breakfast name must be at least {Models.Breakfast.MinNameLenght} and at most {Models.Breakfast.MaxNameLenght} characters long."
    );
  }
}