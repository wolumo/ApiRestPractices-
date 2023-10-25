using ErrorOr;

namespace BuberBreakfast.ServiceErrors; 

public static class Errors
{
  public static class Breakfast 
  {
    public static Error NotFound => Error.NotFound(
        code: "Breakfast.NotFound",
        description: "Breakfast not found"
    );

    public static Error InvalidName => Error.Validation(
      code: "BreakFast.InvalidName",
      description: $"BreakFast name must be at least {Models.Breakfast.MinNameLength} " 
      + $" characters long and at most {Models.Breakfast.MaxNameLength} characters long.");


    public static Error InvalidDescription => Error.Validation(
      code: "BreakFast.InvalidDescription",
      description: $"BreakFast Description must be at least {Models.Breakfast.MinDescriptionLength} " 
      + $"  characters long and at most {Models.Breakfast.MaxDescriptionLength} characters long.");


  }  
}