namespace BuberBreakfast.Contracts.BuberBreakfast;

public record CreateBreakfastRequest(
    string Name,
    string Description,
    DateTime StarDateTime,
    DateTime EndDateTime,
    List<string> Savory,
    List<string> Sweet);