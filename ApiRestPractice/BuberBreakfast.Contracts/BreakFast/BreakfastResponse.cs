namespace BuberBreakfast.Contracts.BuberBreakfast;

public record BreakfastResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime StarDateTime,
    DateTime EndDateTime,
    DateTime lasModifiedDateTime,
    List<string> Savory,
    List<string> Sweet);